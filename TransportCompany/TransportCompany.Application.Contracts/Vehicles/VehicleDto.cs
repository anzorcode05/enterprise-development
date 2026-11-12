
using TransportCompany.Application.Contracts.VehicleModels;

namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>Полная информация о транспортном средстве</summary>
/// <param name="Id">Идентификатор транспорта</param>
/// <param name="LicensePlate">Регистрационный номер</param>
/// <param name="Brand">Марка транспорта</param>
/// <param name="LoadCapacity">Грузоподъёмность</param>
/// <param name="VehicleModel">Модель транспортного средства</param>
public sealed record VehicleDto(
    int Id,
    string LicensePlate,
    string Brand,
    double LoadCapacity,
    VehicleModelDto VehicleModel);
