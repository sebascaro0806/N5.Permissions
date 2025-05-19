using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5.Permissions.Api.Controllers.Common;
using N5.Permissions.Domain.Dtos.GetAll;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Dtos.Request;

namespace N5.Permissions.Api.Controllers;

/// <summary>
/// Controller for managing permissions.
/// This class is responsible for handling HTTP requests related to permissions.
/// It provides endpoints for modifying permissions.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PermissionController(IMediator _mediator, ILogger<PermissionController> _logger) : BaseController(_logger)
{
        /// <summary>
    /// Retrieves a permission by its ID.
    /// This endpoint is responsible for retrieving a specific permission by its ID.
    /// It returns a <see cref="PermissionDto"/> object.
    /// </summary>
    /// <param name="id">The ID of the permission to retrieve.</param>  
    /// <param name="token">The cancellation token.</param>
    [HttpPost]
    public Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommandDto dto, CancellationToken token)
    {
        return HandleOperationAsync("RequestPermission", async () =>
        {
            var result = await _mediator.Send(dto, token);
            return Ok(result);
        });
    }

    /// <summary>
    /// Modifies a permission.
    /// This endpoint is responsible for modifying the details of a permission.
    /// It accepts a <see cref="ModifyPermissionDto"/> object in the request body.
    /// </summary>
    /// <param name="command">The command containing the details of the permission to be modified.</param>
    [HttpPut("{id}")]
    public Task<IActionResult> ModifyPermission([FromRoute] int id, [FromBody] ModifyPermissionDto dto, CancellationToken token)
    {
        return HandleOperationAsync("ModifyPermission", async () =>
        {
            var command = new ModifyPermissionCommandDto { Id = id, Data = dto};
            var result = await _mediator.Send(command, token);

            return Ok(result);
        });
    }

    /// <summary>
    /// Retrieves all permissions.
    /// This endpoint is responsible for retrieving a list of all permissions.
    /// It returns a list of <see cref="PermissionDto"/> objects.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    [HttpGet]
    public Task<IActionResult> GetAllPermissions(CancellationToken token)
    {
        return HandleOperationAsync("GetAllPermissions", async () =>
        {
            var result = await _mediator.Send(new GetAllQueryDto(), token);
            return Ok(result);
        });
    }
}