using Microsoft.AspNetCore.Mvc;

namespace N5.Permissions.Api.Controllers.Common;

/// <summary>
/// Base controller class for all controllers.
/// This class provides common functionality for logging and error handling.
/// </summary>
/// <param name="_logger">The logger instance for logging operations.</param>
public class BaseController(ILogger _logger) : ControllerBase
{
    /// <summary>
    /// Executes an action with logging.
    /// This method logs the start and end of the operation, as well as any exceptions that occur.
    /// </summary>
    /// <param name="operationName">The name of the operation being performed.</param>
    /// <param name="action">The action to be executed.</param>
    protected async Task<IActionResult> HandleOperationAsync(string operationName, Func<Task<IActionResult>> action)
    {
        try
        {
            _logger.LogInformation("Operation {Operation} started.", operationName);

            var result = await action();

            _logger.LogInformation("Operation {Operation} completed successfully.", operationName);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Operation {Operation} failed.", operationName);
            return BadRequest("An error occurred while processing your request.");
        }
    }
}