using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Almeshkat_Online_Schools.Utilities
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var requestUrl = $"{context.Request.Method} {context.Request.Path}{context.Request.QueryString}";

                // Log error details using Serilog
                Log.Error(ex, "An error occurred while processing the request. URL: {RequestUrl}", requestUrl);

                await HandleExceptionAsync(context, ex, requestUrl);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, string requestUrl)
        {
            context.Response.ContentType = "application/json";

            var (message, statusCode) = exception switch
            {
                KeyNotFoundException => ("Resource not found.", HttpStatusCode.NotFound),
                InvalidOperationException => (exception.Message, HttpStatusCode.BadRequest),
                _ => (exception.Message, HttpStatusCode.InternalServerError)
            };

            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = message,
                Url = requestUrl
            });

            await context.Response.WriteAsync(result);
        }
    }
}
