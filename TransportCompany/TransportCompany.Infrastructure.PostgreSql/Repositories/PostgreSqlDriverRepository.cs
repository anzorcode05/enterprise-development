
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.ReadModels;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Репозиторий водителей PostgreSQL
/// </summary>
public sealed class PostgreSqlDriverRepository(
    TransportCompanyDbContext context)
    : PostgreSqlRepository<Driver>(context), IDriverRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<DriverTripCountSummary>> GetTopByTripCountAsync(
        int count,
        CancellationToken cancellationToken = default)
    {
        var summaries = await Context.Trips
            .GroupBy(trip => new
            {
                trip.DriverId,
                trip.Driver.FullName
            })
            .Select(group => new
            {
                group.Key.DriverId,
                DriverName = group.Key.FullName,
                TripCount = group.Count()
            })
            .OrderByDescending(summary => summary.TripCount)
            .ThenBy(summary => summary.DriverId)
            .Take(count)
            .ToListAsync(cancellationToken);

        return summaries
            .Select(summary => new DriverTripCountSummary(
                summary.DriverId,
                summary.DriverName,
                summary.TripCount))
            .ToList();
    }

    /// <inheritdoc/>
    public Task<bool> HasTripsAsync(
        int driverId,
        CancellationToken cancellationToken = default)
    {
        return Context.Trips.AnyAsync(
            trip => trip.DriverId == driverId,
            cancellationToken);
    }
}
