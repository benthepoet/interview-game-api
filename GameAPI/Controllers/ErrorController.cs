using GameAPI.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace GameAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private HttpStatusCode GetStatusCode(Exception exception)
        {
            var statusCode = exception switch
            {
                DuplicateEntityException _ => HttpStatusCode.Conflict,
                EntityNotFoundException _ => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            return statusCode;
        }

        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = GetStatusCode(context.Error);

            return Problem(
                statusCode: (int)statusCode,
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = GetStatusCode(context.Error);

            return Problem(statusCode: (int)statusCode, title: context.Error.Message);
        }
    }
}
