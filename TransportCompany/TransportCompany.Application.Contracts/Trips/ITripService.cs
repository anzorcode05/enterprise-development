
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
    /// <param name="clientId">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список рейсов клиента</returns>
    Task<IReadOnlyList<TripDto>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает рейсы указанного водителя
    /// </summary>
    /// <param name="driverId">Идентификатор водителя</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список рейсов водителя</returns>
    Task<IReadOnlyList<TripDto>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default);
}
