
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using TransportCompany.Application;
using TransportCompany.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInMemoryStorage();
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    var logger = context.RequestServices
        .GetRequiredService<ILoggerFactory>()
        .CreateLogger("ApiExceptionHandler");

    var (statusCode, title) = exception switch
    {
        ArgumentException => (
            StatusCodes.Status400BadRequest,
            "Некорректные данные запроса"),

        KeyNotFoundException => (
            StatusCodes.Status400BadRequest,
            "Связанная запись не найдена"),

        InvalidOperationException => (
            StatusCodes.Status409Conflict,
            "Операция конфликтует с текущими данными"),

        _ => (
            StatusCodes.Status500InternalServerError,
            "Внутренняя ошибка сервера")
    };

    if (statusCode >= StatusCodes.Status500InternalServerError)
    {
        logger.LogError(
            exception,
            "Ошибка при обработке {Method} {Path}",
            context.Request.Method,
            context.Request.Path);
    }
    else
    {
        logger.LogWarning(
            exception,
            "Запрос {Method} {Path} завершился с кодом {StatusCode}",
            context.Request.Method,
            context.Request.Path,
            statusCode);
    }

    context.Response.StatusCode = statusCode;

    await context.Response.WriteAsJsonAsync(
        new ProblemDetails
        {
            Status = statusCode,
            Title = title
        },
        context.RequestAborted);
}));

app.MapControllers();

app.Run();
