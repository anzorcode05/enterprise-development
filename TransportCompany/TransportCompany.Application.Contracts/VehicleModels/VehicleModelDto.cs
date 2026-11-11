
using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.VehicleModels;

/// <summary>
/// Данные модели транспортного средства
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="BodyType">Тип кузова</param>
/// <param name="BodyVolume">Объём кузова</param>
public sealed record VehicleModelDto(
    int Id,
    BodyType BodyType,
    double BodyVolume);
