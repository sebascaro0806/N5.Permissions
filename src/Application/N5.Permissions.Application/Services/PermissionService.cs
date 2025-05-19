using AutoMapper;
using N5.Permissions.Domain;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Dtos.Request;
using N5.Permissions.Domain.Entities;
using N5.Permissions.Domain.Interfaces.Application.Services;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;
using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;

namespace N5.Permissions.Application.Services;

/// <summary>
/// Service for managing permissions.
/// This class is responsible for modifying permissions.
/// </summary>
/// <param name="_mapper">The mapper for mapping between DTOs and entities.</param>
/// <param name="_eventProducer">The event producer for publishing events.</param>
/// <param name="_unitOfWork">The unit of work for managing database transactions.</param>
/// <exception cref="ArgumentNullException">Thrown when the permission is not found.</exception>
public class PermissionService(IMapper _mapper, IEventProducer _eventProducer, IUnitOfWork _unitOfWork) : IPermissionService
{
    public async Task<bool> ModifyPermissionAsync(ModifyPermissionCommandDto modifyPermissionCommandDto, CancellationToken cancellationToken)
    {
        var permission = await _unitOfWork
            .Repository<Permission>()
            .GetByIdAsync(modifyPermissionCommandDto.Id, cancellationToken);

        if (permission == null)
            throw new ArgumentNullException(nameof(permission), "Permission not found");

        var permissionToUpdate = _mapper.Map(modifyPermissionCommandDto.Data, permission);

        await UpdatePermissionAsync(permissionToUpdate, cancellationToken);
        _ = _eventProducer.PublishAsync(CreateEventDto("update", permissionToUpdate), Topics.ModifyPermission);

        return true;
    }

    /// <summary>
    /// Retrieves all permissions asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of permission data transfer objects.</returns>
    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork
            .Repository<Permission>()
            .GetAllAsync(cancellationToken);

        var permissionDtos = _mapper.Map<IEnumerable<PermissionDto>>(permissions);

        _ = _eventProducer.PublishAsync(CreateEventDto("get", permissionDtos), Topics.GetAllPermissions);

        return permissionDtos;
    }

    /// <summary>
    /// Retrieves a permission by its ID asynchronously.
    /// </summary>
    /// <param name="permissionDto">The permission data transfer object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task<PermissionDto> RequestPermission(RequestPermissionCommandDto permissionDto, CancellationToken cancellationToken)
    {
        var result = _unitOfWork.Repository<Permission>()
            .Add(_mapper.Map<Permission>(permissionDto));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _ = _eventProducer.PublishAsync(CreateEventDto("request", result), Topics.RequestPermission);

        return _mapper.Map<PermissionDto>(result);
    }

    /// <summary>
    /// Asynchronously updates the permission in the database.
    /// </summary>
    /// <param name="permission">The permission to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task UpdatePermissionAsync(Permission permission, CancellationToken cancellationToken)
    {
        _unitOfWork
            .Repository<Permission>()
            .Update(permission);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Creates an event data transfer object.
    /// </summary>
    /// <param name="name">The name of the operation.</param>
    /// <param name="data">The data associated with the event.</param>
    private PermissionEventDto CreateEventDto(string name, object data)
    {
        return new PermissionEventDto
        {
            NameOperation = name,
            Data = data
        };
    }
}
