using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult HandleError()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
                _logger.LogError(error.Error, "An unhandled exception occurred.");
                return Problem(
                    title: "An error occurred while processing your request.",
                    detail: error.Error.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return Problem("An unknown error occurred.");
        }
    }
}
