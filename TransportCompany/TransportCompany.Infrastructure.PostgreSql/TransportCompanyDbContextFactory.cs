
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TransportCompany.Infrastructure.PostgreSql;

/// <summary>
/// Создаёт контекст базы данных для инструментов Entity Framework Core
/// </summary>
public sealed class TransportCompanyDbContextFactory
    : IDesignTimeDbContextFactory<TransportCompanyDbContext>
{
    /// <inheritdoc/>
    public TransportCompanyDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable(
                "ConnectionStrings__TransportCompanyDb")
            ?? "Host=localhost;Port=5432;Database=transport_company;Username=postgres;Password=postgres";

        var optionsBuilder =
            new DbContextOptionsBuilder<TransportCompanyDbContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return new TransportCompanyDbContext(optionsBuilder.Options);
    }
}
