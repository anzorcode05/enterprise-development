
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.ReadModels;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Репозиторий клиентов PostgreSQL
/// </summary>
public sealed class PostgreSqlClientRepository(
    TransportCompanyDbContext context)
    : PostgreSqlRepository<Client>(context), IClientRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Client>> GetByVehicleModelAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Trips
            .Where(trip => trip.Vehicle.VehicleModelId == vehicleModelId)
            .Select(trip => trip.Client)
            .Distinct()
            .OrderBy(client => client.Name)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ClientCostSummary>> GetTopByTotalCostAsync(
        int count,
        CancellationToken cancellationToken = default)
    {
        var summaries = await Context.Trips
            .GroupBy(trip => new
            {
                trip.ClientId,
                trip.Client.Name
            })
            .Select(group => new
            {
                group.Key.ClientId,
                ClientName = group.Key.Name,
                TotalCost = group.Sum(trip => trip.Cost)
            })
            .OrderByDescending(summary => summary.TotalCost)
            .ThenBy(summary => summary.ClientName)
            .Take(count)
            .ToListAsync(cancellationToken);

        return summaries
            .Select(summary => new ClientCostSummary(
                summary.ClientId,
                summary.ClientName,
                summary.TotalCost))
            .ToList();
    }

    /// <inheritdoc/>
    public Task<bool> HasTripsAsync(
        int clientId,
        CancellationToken cancellationToken = default)
    {
        return Context.Trips.AnyAsync(
            trip => trip.ClientId == clientId,
            cancellationToken);
    }
}
