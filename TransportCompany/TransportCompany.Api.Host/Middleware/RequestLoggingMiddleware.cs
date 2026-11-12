
namespace TransportCompany.Api.Host.Middleware;

public sealed class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startedAt = DateTime.UtcNow;

        try
        {
            await next(context);
        }
        finally
        {
            var elapsed = DateTime.UtcNow - startedAt;

            logger.LogInformation(
                "HTTP {Method} {Path} завершён со статусом {StatusCode} за {ElapsedMilliseconds} мс",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsed.TotalMilliseconds);
        }
    }
}
