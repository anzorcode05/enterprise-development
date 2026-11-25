using TransportCompany.Domain.Entities;

namespace TransportCompany.Domain.Repositories;

/// <summary>
/// Определяет операции для работы с рейсами
/// </summary>
public interface ITripRepository : IRepository<Trip>
{
    /// <summary>
    /// Возвращает рейсы указанного клиента
    /// </summary>
    Task<IReadOnlyList<Trip>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает рейсы указанного водителя
    /// </summary>
    Task<IReadOnlyList<Trip>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default);
}