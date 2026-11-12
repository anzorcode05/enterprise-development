
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Определяет операции для работы с клиентами
/// </summary>
public interface IClientService
    : ICrudService<int, ClientDto, ClientUpsertDto>
{
    /// <summary>
    /// Возвращает клиентов, использующих указанную модель транспорта
    /// </summary>
    Task<IReadOnlyList<ClientDto>> GetByVehicleModelAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиентов с наибольшей общей стоимостью перевозок
    /// </summary>
    Task<IReadOnlyList<ClientCostDto>> GetTopByTotalCostAsync(
        int count = 5,
        CancellationToken cancellationToken = default);
}
