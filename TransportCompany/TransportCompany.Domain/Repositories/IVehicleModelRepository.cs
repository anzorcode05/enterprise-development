using TransportCompany.Domain.Entities;

namespace TransportCompany.Domain.Repositories;

/// <summary>
/// Определяет операции для работы с моделями транспорта
/// </summary>
public interface IVehicleModelRepository : IRepository<VehicleModel>
{
    /// <summary>
    /// Проверяет наличие транспортных средств указанной модели
    /// </summary>
    Task<bool> HasVehiclesAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default);
}