using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Dtos.Request;

namespace N5.Permissions.Domain.Interfaces.Application.Services;

/// <summary>
/// Interface for the permission service.
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Creates a new permission asynchronously.
    /// </summary>
    /// <param name="modifyPermissionCommandDto">The permission data transfer object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
    Task<bool> ModifyPermissionAsync(ModifyPermissionCommandDto modifyPermissionCommandDto, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all permissions asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of permission data transfer objects.</returns>
    Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Requests a permission asynchronously.
    /// </summary>
    /// <param name="permissionDto">The permission data transfer object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<PermissionDto> RequestPermission(RequestPermissionCommandDto permissionDto, CancellationToken cancellationToken);
}
