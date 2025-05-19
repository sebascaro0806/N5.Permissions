using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;
using N5.Permissions.Infraestructure.Messaging.Kafka.Configuration;
using N5.Permissions.Infraestructure.Messaging.Kafka.Events;
using N5.Permissions.Infraestructure.Messaging.Kafka.Services;

namespace N5.Permissions.Infraestructure.Messaging.Kafka;

/// <summary>
/// Dependency injection extensions for Kafka messaging.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Kafka messaging services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddKafkaMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var setting = configuration.GetSection("Kafka").Get<KafkaSettings>();
        var config = new ConsumerConfig
        {
            BootstrapServers = setting?.BootstrapServers,
            GroupId = setting?.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        services.AddTransient<IConsumer<Ignore, string>>((service) =>
        {
            return new ConsumerBuilder<Ignore, string>(config).Build();
        });

        services.AddTransient<IProducer<Null, string>>((service) =>
        {
            return new ProducerBuilder<Null, string>(config).Build();
        });

        services.AddTransient<IAdminClient>((service) =>
        {
            return new AdminClientBuilder(config).Build();
        });

        services.AddSingleton<IEventProducer, KafkaProducer>();
        services.AddHostedService<KafkaConsumerOrchestratorService>();  

        return services;
    }
}
