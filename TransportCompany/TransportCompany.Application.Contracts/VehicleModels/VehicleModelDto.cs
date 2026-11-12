using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.VehicleModels;

/// <summary>
/// Данные модели транспортного средства
/// </summary>
public sealed record VehicleModelDto(
    int Id,
    BodyType BodyType,
    double BodyVolume);