
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Определяет операции для работы с водителями
/// </summary>
public interface IDriverService
    : ICrudService<int, DriverDto, DriverUpsertDto>
{
    /// <summary>
    /// Возвращает водителей с наибольшим количеством рейсов
    /// </summary>
    Task<IReadOnlyList<DriverTripCountDto>> GetTopByTripCountAsync(
        int count = 5,
        CancellationToken cancellationToken = default);
}
