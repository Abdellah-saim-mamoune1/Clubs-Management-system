using EventsManagement.Dtos;
using System.Net;

namespace EventsManagement.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ServiceResponseDto<object> { Status=401};
                    await context.Response.WriteAsJsonAsync(response);
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ServiceResponseDto<object> { Status = 404 };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unhandled exception occurred. Path: {Path}, Method: {Method}",
                    context.Request.Path,
                    context.Request.Method);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ServiceResponseDto<object> { Status = 500 };
                await context.Response.WriteAsJsonAsync(response);
            }
        }


    }
}
