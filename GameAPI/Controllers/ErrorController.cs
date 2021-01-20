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
                InvalidParameterException _ => HttpStatusCode.BadRequest,
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
            var exception = context.Error;
            var statusCode = GetStatusCode(exception);

            return Problem(
                statusCode: (int)statusCode,
                detail: exception.StackTrace,
                title: exception.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var statusCode = GetStatusCode(exception);

            return Problem(statusCode: (int)statusCode, title: exception.Message);
        }
    }
}
