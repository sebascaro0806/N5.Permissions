namespace N5.Permissions.Infraestructure.Messaging.Kafka.Configuration;

/// <summary>
/// Configuration settings for the Kafka server.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class KafkaTopicAttribute : Attribute
{
    /// <summary>
    /// The name of the Kafka topic.
    /// </summary>
    public string TopicName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaTopicAttribute"/> class with the specified topic name.
    /// </summary>
    /// <param name="topicName">The name of the Kafka topic.</param>
    public KafkaTopicAttribute(string topicName)
    {
        TopicName = topicName;
    }
}