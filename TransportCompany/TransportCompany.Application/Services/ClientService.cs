
using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public sealed class ClientService(IClientRepository clients)
    : CrudService<Client, ClientDto, ClientUpsertDto>(clients), IClientService
{
    /// <inheritdoc/>
    protected override ClientDto ToDto(Client client) =>
        new(client.Id, client.Name, client.Phone);

    /// <inheritdoc/>
    protected override Task<Client> CreateEntityAsync(
        ClientUpsertDto request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new Client
        {
            Name = request.Name.Trim(),
            Phone = request.Phone.Trim()
        });
    }

    /// <inheritdoc/>
    protected override Task UpdateEntityAsync(
        Client client,
        ClientUpsertDto request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

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
        var clientsList = await clients.GetByVehicleModelAsync(
            vehicleModelId,
            cancellationToken);

        return clientsList.Select(ToDto).ToList();
    }

    /// <summary>
    /// Возвращает клиентов с наибольшей общей стоимостью рейсов
    /// </summary>
    public async Task<IReadOnlyList<ClientCostDto>> GetTopByTotalCostAsync(
        int count = 5,
        CancellationToken cancellationToken = default)
    {
        if (count is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                "Количество клиентов должно быть от 1 до 100");
        }

        var summaries = await clients.GetTopByTotalCostAsync(
            count,
            cancellationToken);

        return summaries
            .Select(item => new ClientCostDto(
                item.ClientId,
                item.ClientName,
                item.TotalCost))
            .ToList();
    }

    /// <summary>
    /// Удаляет клиента, если у него нет связанных рейсов
    /// </summary>
    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (await Repository.GetByIdAsync(id, cancellationToken) is null)
        {
            return false;
        }

        if (await clients.HasTripsAsync(id, cancellationToken))
        {
            throw new InvalidOperationException(
                "Нельзя удалить клиента, у которого есть рейсы");
        }

        return await Repository.DeleteAsync(id, cancellationToken);
    }
}
