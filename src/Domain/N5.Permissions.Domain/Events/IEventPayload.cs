namespace N5.Permissions.Domain.Events;

/// <summary>
/// Base class for event payloads.
/// This class contains a unique identifier for the event.
/// </summary>
public interface IEventPayload
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    Guid EventId { get; set; }
}