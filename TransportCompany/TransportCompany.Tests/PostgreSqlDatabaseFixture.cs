
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

        await context.InitializeDatabaseAsync(
            TestContext.Current.CancellationToken);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
