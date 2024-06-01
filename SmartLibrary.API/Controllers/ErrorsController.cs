using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Application.Common.Error;

namespace SmartLibrary.API.Controllers
{
    [Route("/error")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                UnauthorizedAccessException unauthorizedAccessException => (StatusCodes.Status403Forbidden, "Your account isn't authorized to perform this action."),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
            };
            return Problem(statusCode: statusCode, title: message);
        }
    }
}
