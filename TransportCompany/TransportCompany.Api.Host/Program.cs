
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TransportCompany.Api.Host.Exceptions;
using TransportCompany.Api.Host.Middleware;
using TransportCompany.Application;
using TransportCompany.Infrastructure.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(
                namingPolicy: null,
                allowIntegerValues: false));
    });

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddPostgreSqlDatabase(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<TransportCompanyDbContext>();

    await dbContext.Database.MigrateAsync();

    await dbContext.Database.ExecuteSqlRawAsync(
        """
        SELECT setval(
            pg_get_serial_sequence('clients', 'id'),
            COALESCE((SELECT MAX(id) FROM clients), 1),
            EXISTS(SELECT 1 FROM clients));

        SELECT setval(
            pg_get_serial_sequence('drivers', 'id'),
            COALESCE((SELECT MAX(id) FROM drivers), 1),
            EXISTS(SELECT 1 FROM drivers));

        SELECT setval(
            pg_get_serial_sequence('vehicle_models', 'id'),
            COALESCE((SELECT MAX(id) FROM vehicle_models), 1),
            EXISTS(SELECT 1 FROM vehicle_models));

        SELECT setval(
            pg_get_serial_sequence('vehicles', 'id'),
            COALESCE((SELECT MAX(id) FROM vehicles), 1),
            EXISTS(SELECT 1 FROM vehicles));

        SELECT setval(
            pg_get_serial_sequence('trips', 'id'),
            COALESCE((SELECT MAX(id) FROM trips), 1),
            EXISTS(SELECT 1 FROM trips));
        """);
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
