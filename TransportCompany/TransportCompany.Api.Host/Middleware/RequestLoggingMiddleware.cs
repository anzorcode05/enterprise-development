
namespace TransportCompany.Api.Host.Middleware;

/// <summary>
/// Записывает сведения об обработанных HTTP-запросах
/// </summary>
public sealed class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    /// <summary>
    /// Передаёт запрос следующему компоненту и записывает результат обработки
    /// </summary>
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
