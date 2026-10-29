using Scalar.AspNetCore;
using TransportCompany.Application.Services;
using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Application.Contracts.Trips;
using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInMemoryStorage();
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IDriverService, DriverService>();
builder.Services.AddSingleton<IVehicleModelService, VehicleModelService>();
builder.Services.AddSingleton<IVehicleService, VehicleService>();
builder.Services.AddSingleton<ITripService, TripService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
        .CreateLogger("ApiExceptionHandler");

    var (statusCode, title) = exception switch
    {
        ArgumentException => (StatusCodes.Status400BadRequest, "Некорректные данные запроса"),
        KeyNotFoundException => (StatusCodes.Status400BadRequest, "Связанная запись не найдена"),
        InvalidOperationException => (StatusCodes.Status409Conflict, "Операция конфликтует с текущими данными"),
        _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера")
    };

    if (statusCode >= StatusCodes.Status500InternalServerError)
    {
        logger.LogError(exception, "Необработанная ошибка при обработке {Method} {Path}",
            context.Request.Method, context.Request.Path);
    }
    else
    {
        logger.LogWarning(exception, "Запрос {Method} {Path} завершился с кодом {StatusCode}",
            context.Request.Method, context.Request.Path, statusCode);
    }

    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/problem+json";
    await context.Response.WriteAsJsonAsync(new Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        Status = statusCode,
        Title = title
    }, context.RequestAborted);
}));
app.MapControllers();
app.Run();
