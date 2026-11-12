
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Определяет операции для работы с рейсами
/// </summary>
public interface ITripService
    : ICrudService<int, TripDto, TripUpsertDto>
{
    /// <summary>
    /// Возвращает рейсы указанного клиента
    /// </summary>
    Task<IReadOnlyList<TripDto>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает рейсы указанного водителя
    /// </summary>
    Task<IReadOnlyList<TripDto>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default);
}
