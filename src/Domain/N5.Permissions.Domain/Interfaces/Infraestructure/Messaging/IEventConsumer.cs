using N5.Permissions.Domain.Events;

namespace N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;

/// <summary>
/// Interface for an event consumer.
/// </summary>
/// <typeparam name="TPayload">The type of the event to consume.</typeparam>
public interface IEventConsumer<in TPayload> where TPayload : IEventPayload
{
    /// <summary>
    /// Consumes an event of type TPayload.
    /// </summary>
    /// <param name="event">The event to consume.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleAsync(TPayload @event, CancellationToken cancellationToken);
}