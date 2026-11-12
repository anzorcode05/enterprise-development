
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
    /// <param name="vehicleModelId">Идентификатор модели транспорта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список подходящих клиентов</returns>
    Task<IReadOnlyList<ClientDto>> GetByVehicleModelAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиентов с наибольшей общей стоимостью перевозок
    /// </summary>
    /// <param name="count">Количество клиентов в результате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список клиентов с суммарной стоимостью перевозок</returns>
    Task<IReadOnlyList<ClientCostDto>> GetTopByTotalCostAsync(
        int count = 5,
        CancellationToken cancellationToken = default);
}
