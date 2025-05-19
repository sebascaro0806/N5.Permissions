using System.Text.Json;
using Confluent.Kafka;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;

namespace N5.Permissions.Infraestructure.Messaging.Kafka.Events;

/// <summary>
/// Kafka producer for sending messages to a Kafka topic.
/// </summary>
public class KafkaProducer(IProducer<Null, string> _producer) : IEventProducer
{
    /// <summary>
    /// Asynchronously publishes a message to the specified Kafka topic.
    /// </summary>
    public async Task PublishAsync<T>(T message, string topic) where T : class
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
    }
}
