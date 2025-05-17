namespace N5.Perimissions.Domain.Entities;

/// <summary>
/// Represents the type of permission.
/// </summary>
public class PermissionTypes
{
    /// <summary>
    /// Gets or sets the unique identifier for the permission type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the permission type.
    /// </summary>
    public required string Description { get; set; }
}
