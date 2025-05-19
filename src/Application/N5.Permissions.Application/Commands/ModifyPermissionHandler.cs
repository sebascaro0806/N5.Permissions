using FluentValidation;
using MediatR;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Interfaces.Application.Services;

namespace N5.Permissions.Application.Commands;

/// <summary>
/// Handler for the <see cref="ModifyPermissionCommandDto"/> class.
/// This class is responsible for handling the modification of permissions.
/// </summary>
/// <param name="_service">The permission service.</param>
public class ModifyPermissionHandler(
    IValidator<ModifyPermissionDto> _validator,
    IPermissionService _service) : IRequestHandler<ModifyPermissionCommandDto, bool>
{
    /// <summary>
    /// Handles the modification of permissions.
    /// This method is called when a request to modify a permission is made.
    /// </summary>
    /// <param name="request">The request containing the details of the permission to be modified.</param>
    public async Task<bool> Handle(ModifyPermissionCommandDto request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request.Data, cancellationToken);
        if (!result.IsValid) throw new ValidationException(result.Errors);

        return await _service.ModifyPermissionAsync(request, cancellationToken);
    }
}
