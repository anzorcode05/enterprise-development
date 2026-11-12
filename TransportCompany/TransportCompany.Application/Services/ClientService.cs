
using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public sealed class ClientService
    : CrudService<Client, ClientDto, ClientUpsertDto>, IClientService
{
    private readonly IRepository<Trip> _trips;

    /// <summary>
    /// Создаёт сервис клиентов
    /// </summary>
    public ClientService(IRepository<Client> clients, IRepository<Trip> trips)
        : base(clients)
    {
        _trips = trips;
    }

    /// <inheritdoc/>
    protected override ClientDto ToDto(Client client) =>
        new(client.Id, client.Name, client.Phone);

    /// <inheritdoc/>
    protected override Task<Client> CreateEntityAsync(ClientUpsertDto request, int id)
    {
        return Task.FromResult(new Client
        {
            Id = id,
            Name = request.Name.Trim(),
            Phone = request.Phone.Trim()
        });
    }

    /// <inheritdoc/>
    protected override Task UpdateEntityAsync(Client client, ClientUpsertDto request)
    {
        client.Name = request.Name.Trim();
        client.Phone = request.Phone.Trim();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Возвращает клиентов, которые пользовались указанной моделью транспорта
    /// </summary>
    public async Task<IReadOnlyList<ClientDto>> GetByVehicleModelAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var trips = await _trips.FindAsync(
            trip => trip.Vehicle.VehicleModel.Id == vehicleModelId);

        return trips
            .Select(trip => trip.Client)
            .GroupBy(client => client.Id)
            .Select(group => group.First())
            .OrderBy(client => client.Name)
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Возвращает клиентов с наибольшей общей стоимостью рейсов
    /// </summary>
    public async Task<IReadOnlyList<ClientCostDto>> GetTopByTotalCostAsync(
        int count = 5,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (count is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                "Количество клиентов должно быть от 1 до 100");
        }

        var trips = await _trips.GetAllAsync();

        return trips
            .GroupBy(trip => trip.Client.Id)
            .Select(group =>
            {
                var client = group.First().Client;

                return new ClientCostDto(
                    client.Id,
                    client.Name,
                    group.Sum(trip => trip.Cost));
            })
            .OrderByDescending(item => item.TotalCost)
            .ThenBy(item => item.ClientName)
            .Take(count)
            .ToList();
    }

    /// <summary>
    /// Удаляет клиента, если у него нет связанных рейсов
    /// </summary>
    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await Repository.GetByIdAsync(id) is null)
        {
            return false;
        }

        var trips = await _trips.FindAsync(trip => trip.Client.Id == id);

        if (trips.Count > 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить клиента, у которого есть рейсы");
        }

        return await Repository.DeleteAsync(id);
    }
}
