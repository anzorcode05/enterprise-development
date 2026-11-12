
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.VehicleModels;

/// <summary>
/// Определяет операции для работы с моделями транспортных средств
/// </summary>
public interface IVehicleModelService
    : ICrudService<int, VehicleModelDto, VehicleModelUpsertDto>
{
}
