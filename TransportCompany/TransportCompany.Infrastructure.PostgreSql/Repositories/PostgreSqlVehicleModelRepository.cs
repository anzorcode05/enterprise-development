
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Репозиторий моделей транспортных средств PostgreSQL
/// </summary>
public sealed class PostgreSqlVehicleModelRepository(
    TransportCompanyDbContext context)
    : PostgreSqlRepository<VehicleModel>(context), IVehicleModelRepository
{
    /// <inheritdoc/>
    public Task<bool> HasVehiclesAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default)
    {
        return Context.Vehicles.AnyAsync(
            vehicle => vehicle.VehicleModelId == vehicleModelId,
            cancellationToken);
    }
}
