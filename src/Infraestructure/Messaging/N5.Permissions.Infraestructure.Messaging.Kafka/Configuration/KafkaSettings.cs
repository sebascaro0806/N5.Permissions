namespace N5.Permissions.Infraestructure.Messaging.Kafka.Configuration;

/// <summary>
/// Configuration settings for the Kafka server.
/// </summary>
public class KafkaSettings
{
    /// <summary>
    /// The URL of the Kafka server.
    /// </summary>
    public string BootstrapServers { get; set; } = string.Empty;

    /// <summary>
    /// The group ID for the Kafka consumer.
    /// </summary>
    public string GroupId { get; set; } = string.Empty;
}