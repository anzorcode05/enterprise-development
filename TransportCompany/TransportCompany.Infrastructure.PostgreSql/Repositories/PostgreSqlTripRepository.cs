
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Репозиторий рейсов PostgreSQL
/// </summary>
public sealed class PostgreSqlTripRepository(
    TransportCompanyDbContext context)
    : PostgreSqlRepository<Trip>(context), ITripRepository
{
    /// <inheritdoc/>
    public override async Task<IReadOnlyList<Trip>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Trips
            .Include(trip => trip.Driver)
            .Include(trip => trip.Vehicle)
                .ThenInclude(vehicle => vehicle.VehicleModel)
            .Include(trip => trip.Client)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<Trip?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Trips
            .Include(trip => trip.Driver)
            .Include(trip => trip.Vehicle)
                .ThenInclude(vehicle => vehicle.VehicleModel)
            .Include(trip => trip.Client)
            .FirstOrDefaultAsync(trip => trip.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trip>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Trips
            .Include(trip => trip.Driver)
            .Include(trip => trip.Vehicle)
                .ThenInclude(vehicle => vehicle.VehicleModel)
            .Include(trip => trip.Client)
            .Where(trip => trip.ClientId == clientId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trip>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Trips
            .Include(trip => trip.Driver)
            .Include(trip => trip.Vehicle)
                .ThenInclude(vehicle => vehicle.VehicleModel)
            .Include(trip => trip.Client)
            .Where(trip => trip.DriverId == driverId)
            .ToListAsync(cancellationToken);
    }
}
