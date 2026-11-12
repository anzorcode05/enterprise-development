using TransportCompany.Application.Contracts.VehicleModels;

namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Полная информация о транспортном средстве
/// </summary>
public sealed record VehicleDto(
    int Id,
    string LicensePlate,
    string Brand,
    double LoadCapacity,
    VehicleModelDto VehicleModel);