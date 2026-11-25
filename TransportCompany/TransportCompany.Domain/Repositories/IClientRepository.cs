using TransportCompany.Domain.Entities;
using TransportCompany.Domain.ReadModels;

namespace TransportCompany.Domain.Repositories;

/// <summary>
/// Определяет операции для работы с клиентами
/// </summary>
public interface IClientRepository : IRepository<Client>
{
    /// <summary>
    /// Возвращает клиентов, которые пользовались указанной моделью транспорта
    /// </summary>
    Task<IReadOnlyList<Client>> GetByVehicleModelAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиентов с наибольшей общей стоимостью рейсов
    /// </summary>
    Task<IReadOnlyList<ClientCostSummary>> GetTopByTotalCostAsync(
        int count,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет наличие рейсов у клиента
    /// </summary>
    Task<bool> HasTripsAsync(
        int clientId,
        CancellationToken cancellationToken = default);
}