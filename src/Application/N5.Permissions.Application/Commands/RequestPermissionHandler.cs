using FluentValidation;
using MediatR;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Request;
using N5.Permissions.Domain.Interfaces.Application.Services;

namespace N5.Permissions.Application.Commands;

/// <summary>
/// Handler for processing permission requests.
/// This class implements the MediatR IRequestHandler interface.
/// It handles the RequestPermissionCommandDto and returns a PermissionDto.
/// </summary>
/// <param name="_service">Permission service for processing requests.</param>
/// <param name="_validator">Validator for validating the request.</param>
public class RequestPermissionHandler(IValidator<RequestPermissionCommandDto> _validator,
    IPermissionService _service) : IRequestHandler<RequestPermissionCommandDto, PermissionDto>
{
    /// <summary>
    /// Handles the request for a permission.
    /// This method validates the request and processes it using the permission service.
    /// </summary>
    /// <param name="request">The request containing permission details.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the permission data transfer object.</returns>
    public async Task<PermissionDto> Handle(RequestPermissionCommandDto request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid) throw new ValidationException(result.Errors);

        return await _service.RequestPermission(request, cancellationToken);
    }
}
