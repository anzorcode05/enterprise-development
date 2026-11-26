
using Microsoft.EntityFrameworkCore;

namespace TransportCompany.Infrastructure.PostgreSql;

/// <summary>
/// Выполняет начальную настройку базы данных
/// </summary>
public static class DatabaseInitializer
{
    private const string IdentitySequencesSql =
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
        """;

    /// <summary>
    /// Применяет миграции и синхронизирует последовательности идентификаторов
    /// </summary>
    public static async Task InitializeDatabaseAsync(
        this TransportCompanyDbContext context,
        CancellationToken cancellationToken = default)
    {
        await context.Database.MigrateAsync(cancellationToken);

        await context.Database.ExecuteSqlRawAsync(
            IdentitySequencesSql,
            cancellationToken);
    }
}
