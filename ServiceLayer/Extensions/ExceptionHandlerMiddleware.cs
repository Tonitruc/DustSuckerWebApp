using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ServiceLayer.Extensions
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            var problem = new ProblemDetails();

            httpContext.Response.ContentType = "application/json";

            if (exception is ValidationException ex)
            {
                problem.Title = "Validations exception.";
                problem.Detail = ex.Message;
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else 
            {
                problem.Title = "Server exception.";
                problem.Detail = exception.Message;
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
