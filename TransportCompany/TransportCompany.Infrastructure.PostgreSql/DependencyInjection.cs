
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TransportCompany.Infrastructure.PostgreSql;

/// <summary>
/// Предоставляет методы регистрации PostgreSQL-инфраструктуры
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует контекст базы данных PostgreSQL
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

        return services;
    }
}
