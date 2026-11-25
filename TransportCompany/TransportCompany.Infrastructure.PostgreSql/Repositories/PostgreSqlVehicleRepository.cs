
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.ReadModels;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Репозиторий транспортных средств PostgreSQL
/// </summary>
public sealed class PostgreSqlVehicleRepository(
    TransportCompanyDbContext context)
    : PostgreSqlRepository<Vehicle>(context), IVehicleRepository
{
    /// <inheritdoc/>
    public override async Task<IReadOnlyList<Vehicle>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Vehicles
            .Include(vehicle => vehicle.VehicleModel)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<Vehicle?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Vehicles
            .Include(vehicle => vehicle.VehicleModel)
            .FirstOrDefaultAsync(vehicle => vehicle.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Vehicle>> GetVehiclesOnTripsAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Vehicles
            .Where(vehicle => Context.Trips.Any(
                trip => trip.VehicleId == vehicle.Id))
            .Include(vehicle => vehicle.VehicleModel)
            .OrderBy(vehicle => vehicle.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<VehicleTripCountSummary>> GetTripCountsAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Vehicles
            .OrderBy(vehicle => vehicle.Id)
            .Select(vehicle => new VehicleTripCountSummary(
                vehicle.Id,
                vehicle.LicensePlate,
                Context.Trips.Count(trip => trip.VehicleId == vehicle.Id)))
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> HasTripsAsync(
        int vehicleId,
        CancellationToken cancellationToken = default)
    {
        return Context.Trips.AnyAsync(
            trip => trip.VehicleId == vehicleId,
            cancellationToken);
    }
}
