
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Определяет операции для работы с транспортными средствами
/// </summary>
public interface IVehicleService
    : ICrudService<int, VehicleDto, VehicleUpsertDto>
{
    /// <summary>
    /// Возвращает транспортные средства, участвовавшие в рейсах
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список транспортных средств</returns>
    Task<IReadOnlyList<VehicleDto>> GetVehiclesOnTripsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает количество рейсов для каждого транспортного средства
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список транспортных средств с количеством рейсов</returns>
    Task<IReadOnlyList<VehicleTripCountDto>> GetTripCountsAsync(
        CancellationToken cancellationToken = default);
}
