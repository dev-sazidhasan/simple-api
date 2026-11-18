using System.Net;
using System.Text.Json;

namespace SimpleNameApi.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "An unexpected error occurred."
            );

            if (context.Response.HasStarted)
            {
                throw;
            }

            var statusCode = exception switch
            {
                UnauthorizedAccessException =>
                    (int)HttpStatusCode.Unauthorized,

                InvalidOperationException =>
                    (int)HttpStatusCode.Conflict,

                ArgumentException =>
                    (int)HttpStatusCode.BadRequest,

                _ =>
                    (int)HttpStatusCode.InternalServerError
            };

            var message = statusCode switch
            {
                401 => exception.Message,
                409 => exception.Message,
                400 => exception.Message,
                _ => "An unexpected error occurred."
            };

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                statusCode,
                message
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}