namespace TransportCompany.Application.Contracts.VehicleModels;

/// <summary>
/// Данные модели транспортного средства.
/// </summary>
/// <param name="Id">идентификатор модели</param>
/// <param name="BodyType">тип кузова</param>
/// <param name="BodyVolume">объём кузова</param>
public sealed record VehicleModelDto(int Id, string BodyType, double BodyVolume);
