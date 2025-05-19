using N5.Permissions.Domain.Dtos.Modify;

namespace N5.Permissions.Domain.Dtos.Common;

/// <summary>
/// Data transfer object for a permission.
/// This class contains the properties required to represent a permission.
/// </summary>
public record PermissionDto : ModifyPermissionDto
{
    /// <summary>
    /// Gets or sets the identifier of the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of permission.
    /// </summary>
    public int PermissionType { get; set; }
}
