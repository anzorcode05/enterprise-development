
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using TransportCompany.Infrastructure.PostgreSql;

namespace TransportCompany.Tests;

/// <summary>
/// Подготавливает временную базу PostgreSQL для интеграционных тестов
/// </summary>
public sealed class PostgreSqlDatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container =
        new PostgreSqlBuilder("postgres:17-alpine")
            .WithDatabase("transport_company_tests")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

    /// <summary>
    /// Создаёт контекст тестовой базы данных
    /// </summary>
    public TransportCompanyDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TransportCompanyDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        return new TransportCompanyDbContext(options);
    }

    /// <inheritdoc/>
    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();

        await using var context = CreateDbContext();
        await context.Database.MigrateAsync();

        await context.Database.ExecuteSqlRawAsync(
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
            """,
            TestContext.Current.CancellationToken);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
