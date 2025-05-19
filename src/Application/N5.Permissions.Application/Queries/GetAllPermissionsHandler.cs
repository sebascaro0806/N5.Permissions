using MediatR;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.GetAll;
using N5.Permissions.Domain.Interfaces.Application.Services;

namespace N5.Permissions.Application.Queries;

/// <summary>
/// Handler for the GetAllCommandDto request.
/// This class is responsible for handling the retrieval of all permissions.
/// </summary>
/// <param name="_service">The permission service.</param>
public class GetAllPermissionsHandler(IPermissionService _service) : IRequestHandler<GetAllQueryDto, IEnumerable<PermissionDto>>
{
    /// <summary>
    /// Handles the request to get all permissions.
    /// </summary>
    /// <param name="request"> The request containing the details of the permissions to be retrieved.</param>
    /// <param name="cancellationToken"> The cancellation token.</param>
    /// <returns> A task that represents the asynchronous operation. The task result contains a list of permission data transfer objects.</returns>
    public async Task<IEnumerable<PermissionDto>> Handle(GetAllQueryDto request, CancellationToken cancellationToken)
    {
        return await _service.GetAllPermissionsAsync(cancellationToken);
    }
}