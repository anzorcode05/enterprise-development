
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;
using TransportCompany.Infrastructure.PostgreSql.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql;

/// <summary>
/// Предоставляет методы регистрации PostgreSQL-инфраструктуры
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует контекст базы данных и репозитории PostgreSQL
    /// </summary>
    public static IServiceCollection AddPostgreSqlDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(
            "TransportCompanyDb");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Не найдена строка подключения TransportCompanyDb");
        }

        services.AddDbContext<TransportCompanyDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<PostgreSqlClientRepository>();
        services.AddScoped<PostgreSqlDriverRepository>();
        services.AddScoped<PostgreSqlTripRepository>();
        services.AddScoped<PostgreSqlVehicleRepository>();
        services.AddScoped<PostgreSqlVehicleModelRepository>();

        services.AddScoped<IClientRepository>(
            provider => provider.GetRequiredService<PostgreSqlClientRepository>());
        services.AddScoped<IDriverRepository>(
            provider => provider.GetRequiredService<PostgreSqlDriverRepository>());
        services.AddScoped<ITripRepository>(
            provider => provider.GetRequiredService<PostgreSqlTripRepository>());
        services.AddScoped<IVehicleRepository>(
            provider => provider.GetRequiredService<PostgreSqlVehicleRepository>());
        services.AddScoped<IVehicleModelRepository>(
            provider => provider.GetRequiredService<PostgreSqlVehicleModelRepository>());

        services.AddScoped<IRepository<Client>>(
            provider => provider.GetRequiredService<PostgreSqlClientRepository>());
        services.AddScoped<IRepository<Driver>>(
            provider => provider.GetRequiredService<PostgreSqlDriverRepository>());
        services.AddScoped<IRepository<Trip>>(
            provider => provider.GetRequiredService<PostgreSqlTripRepository>());
        services.AddScoped<IRepository<Vehicle>>(
            provider => provider.GetRequiredService<PostgreSqlVehicleRepository>());
        services.AddScoped<IRepository<VehicleModel>>(
            provider => provider.GetRequiredService<PostgreSqlVehicleModelRepository>());

        return services;
    }
}
