using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Server.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult HandleError()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
                return Problem(
                    title: "An error occurred while processing your request.",
                    detail: error.Error.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return Problem("An unknown error occurred.");
        }
    }
}
