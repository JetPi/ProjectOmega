using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace backend.Controllers;

public abstract class BaseController : ControllerBase
{
    protected ActionResult HandleError(List<Error> errors)
    {
        var firstError = errors.First();
        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError // Fallback
        };
        return Problem(statusCode: statusCode, detail: firstError.Description);
        
    }
}