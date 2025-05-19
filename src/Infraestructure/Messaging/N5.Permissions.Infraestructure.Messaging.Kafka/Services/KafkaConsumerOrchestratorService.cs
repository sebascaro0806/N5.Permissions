using System.Reflection;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;
using N5.Permissions.Infraestructure.Messaging.Kafka.Configuration;

namespace N5.Permissions.Infraestructure.Messaging.Kafka.Services;

/// <summary>
/// Kafka consumer orchestrator service.
/// This service is responsible for consuming messages from Kafka topics and invoking the appropriate event handlers.
/// </summary>
/// <param name="_serviceProvider">The service provider for resolving dependencies.</param> 
/// <param name="_logger">The logger instance.</param>
/// <param name="_consumer">The Kafka consumer instance.</param>
public class KafkaConsumerOrchestratorService
    (IServiceProvider _serviceProvider,
    ILogger<KafkaConsumerOrchestratorService> _logger,
    IAdminClient _adminClient,
    IConsumer<Ignore, string> _consumer) : BackgroundService
{
    /// <summary>
    /// Executes the background service.
    /// </summary>
    /// <param name="stoppingToken">The cancellation token.</param>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var handlers = GetEventConsumersWithTopics();

        try
        {
            if (handlers.Any())
            {
                _consumer.Subscribe(handlers.Keys);
            }
            else
            {
                _logger.LogInformation("No event consumers found.");
                return;
            }


            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);

                if (handlers.TryGetValue(result.Topic, out var handlerEntry))
                {
                    await InvokeHandlerAsync(handlerEntry, result, stoppingToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogError("Consumer stopped.");
        }
        finally
        {
            _consumer.Close();
        }
    }

    /// <summary>
    /// Invokes the event handler for the consumed message.
    /// </summary>
    /// <param name="handlerEntry">The handler entry containing the handler type and payload type.</param>
    /// <param name="result">The consumed message result.</param>
    /// <param name="stoppingToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task InvokeHandlerAsync((
        Type handlerType, Type payloadType) handlerEntry,
        ConsumeResult<Ignore, string> result,
        CancellationToken stoppingToken)
    {
        var (handlerType, payloadType) = handlerEntry;

        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        try
        {
            var message = JsonSerializer.Deserialize(result.Message.Value, payloadType);

            if (message != null)
            {
                var method = handlerType.GetMethod("HandleAsync");
                if (method == null)
                    throw new InvalidOperationException($"HandleAsync method not found in {handlerType.Name}");

                var task = (Task)method.Invoke(handler, new[] { message, stoppingToken })!;
                await task;
            }
        }
        catch (JsonException e)
        {
            _logger.LogError($"Failed to deserialize message for topic {result.Topic}: {e.Message}");
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while processing message for topic {result.Topic}: {e.Message}");
        }
        finally
        {
            _consumer.Commit(result);
        }
    }

    /// <summary>
    /// Retrieves all event consumers with their associated topics.
    /// </summary>
    /// <returns>A dictionary where the key is the topic name and the value is a tuple containing the handler type and payload type.</returns>
    private Dictionary<string, (Type handlerType, Type payloadType)> GetEventConsumersWithTopics()
    {
        var dict = new Dictionary<string, (Type, Type)>();

        var consumerTypesWithTopics = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventConsumer<>)))
            .Select(t => new
            {
                Type = t,
                Topics = t.GetCustomAttributes<KafkaTopicAttribute>().Select(attr => attr.TopicName).ToList()
            })
            .Where(x => x.Topics.Any());

        CreateTopics(consumerTypesWithTopics.SelectMany(x => x.Topics).ToList());

        foreach (var consumer in consumerTypesWithTopics)
        {
            var iface = consumer.Type.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventConsumer<>));

            var payloadType = iface.GetGenericArguments().First();

            foreach (var topic in consumer.Topics)
            {
                dict[topic] = (consumer.Type, payloadType);
            }
        }

        return dict;
    }

    /// <summary>
    /// Creates Kafka topics based on the provided list of topic names.
    /// </summary>
    /// <param name="topics">The list of topic names to create.</param>
    private void CreateTopics(List<string> topics)
    {
        foreach (var topic in topics)
        {
            try
            {
                _adminClient.GetMetadata(topic, new TimeSpan(0, 1, 0));
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured creating topic {topic}: {e.Message}");
            }
        }
    }
}
