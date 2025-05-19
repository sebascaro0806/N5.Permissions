using MediatR;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Modify;

namespace N5.Permissions.Domain.Dtos.Request;

/// <summary>
/// Data transfer object for requesting a permission.
/// </summary>
public record RequestPermissionCommandDto : ModifyPermissionDto, IRequest<PermissionDto>
{
    /// <summary>
    /// Gets or sets the type of the permission.
    /// </summary>
    public int PermissionType { get; set; }
}