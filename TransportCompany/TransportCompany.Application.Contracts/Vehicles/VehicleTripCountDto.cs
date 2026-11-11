
namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Результат аналитики по количеству рейсов транспортного средства
/// </summary>
/// <param name="VehicleId">Идентификатор транспорта</param>
/// <param name="LicensePlate">Регистрационный номер</param>
/// <param name="TripCount">Количество рейсов</param>
public sealed record VehicleTripCountDto(
    int VehicleId,
    string LicensePlate,
    int TripCount);
