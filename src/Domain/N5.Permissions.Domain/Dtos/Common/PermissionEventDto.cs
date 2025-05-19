using N5.Permissions.Domain.Events;

namespace N5.Permissions.Domain.Dtos.Common;

/// <summary>
/// Represents a data transfer object for permission events.
/// This class implements the IEventPayload interface.
public class PermissionEventDto : IEventPayload
{
    /// <summary>
    /// Gets or sets the name of the operation associated with the event.
    /// </summary>
    public string NameOperation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    public object Data { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();
}