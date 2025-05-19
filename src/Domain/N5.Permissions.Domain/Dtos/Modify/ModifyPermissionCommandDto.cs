using MediatR;

namespace N5.Permissions.Domain.Dtos.Modify;

/// <summary>
/// Data transfer object for modifying a permission.
/// This class contains the properties required to modify a permission.
public record ModifyPermissionCommandDto : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the identifier of the permission to be modified.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets details of the permission to be modified.
    /// </summary>
    public ModifyPermissionDto Data { get; set; } = default!;
}
