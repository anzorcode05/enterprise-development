namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Результат аналитики по количеству рейсов транспортного средства.
/// </summary>
/// <param name="VehicleId">идентификатор транспорта</param>
/// <param name="LicensePlate">регистрационный номер</param>
/// <param name="TripCount">количество рейсов</param>
public sealed record VehicleTripCountDto(int VehicleId, string LicensePlate, int TripCount);
