using TransportCompany.Domain.Entities;
using TransportCompany.Domain.ReadModels;

namespace TransportCompany.Domain.Repositories;

/// <summary>
/// Определяет операции для работы с транспортными средствами
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle>
{
    /// <summary>
    /// Возвращает транспортные средства, участвовавшие в рейсах
    /// </summary>
    Task<IReadOnlyList<Vehicle>> GetVehiclesOnTripsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает количество рейсов для каждого транспортного средства
    /// </summary>
    Task<IReadOnlyList<VehicleTripCountSummary>> GetTripCountsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет наличие рейсов у транспортного средства
    /// </summary>
    Task<bool> HasTripsAsync(
        int vehicleId,
        CancellationToken cancellationToken = default);
}