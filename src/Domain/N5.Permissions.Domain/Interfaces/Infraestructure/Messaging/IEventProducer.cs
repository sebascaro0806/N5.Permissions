namespace N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;

/// <summary>
/// Interface for the Event Producer.
/// This interface defines a contract for publishing events to a messaging system.
/// </summary>
public interface IEventProducer
{
    /// <summary>
    /// Asynchronously publishes a message to a specified topic.
    /// </summary>
    Task PublishAsync<T>(T message, string topic) where T : class;
}