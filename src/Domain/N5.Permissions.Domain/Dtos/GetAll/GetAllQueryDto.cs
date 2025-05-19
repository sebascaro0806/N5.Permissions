using MediatR;
using N5.Permissions.Domain.Dtos.Common;

namespace N5.Permissions.Domain.Dtos.GetAll;

/// <summary>
/// Data transfer object for getting all permissions.
/// This class is used to request all permissions.
/// </summary>
public record GetAllQueryDto : IRequest<IEnumerable<PermissionDto>> { }
