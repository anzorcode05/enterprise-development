using TransportCompany.Domain.Entities;
using TransportCompany.Domain.ReadModels;

namespace TransportCompany.Domain.Repositories;

/// <summary>
/// Определяет операции для работы с водителями
/// </summary>
public interface IDriverRepository : IRepository<Driver>
{
    /// <summary>
    /// Возвращает водителей с наибольшим количеством рейсов
    /// </summary>
    Task<IReadOnlyList<DriverTripCountSummary>> GetTopByTripCountAsync(
        int count,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет наличие рейсов у водителя
    /// </summary>
    Task<bool> HasTripsAsync(
        int driverId,
        CancellationToken cancellationToken = default);
}