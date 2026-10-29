namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Полная информация о транспортном средстве.
/// </summary>
/// <param name="Id">идентификатор транспорта</param>
/// <param name="LicensePlate">регистрационный номер</param>
/// <param name="Brand">марка</param>
/// <param name="LoadCapacity">грузоподъёмность</param>
/// <param name="VehicleModelId">идентификатор модели</param>
/// <param name="VehicleModelBodyType">тип кузова модели</param>
/// <param name="VehicleModelBodyVolume">объём кузова модели</param>
public sealed record VehicleDto(int Id, string LicensePlate, string Brand, double LoadCapacity, int VehicleModelId, string VehicleModelBodyType, double VehicleModelBodyVolume);
