using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Изменяемые данные транспортного средства.
/// </summary>
/// <param name="LicensePlate">регистрационный номер</param>
/// <param name="Brand">марка транспорта</param>
/// <param name="LoadCapacity">положительная грузоподъёмность</param>
/// <param name="VehicleModelId">идентификатор существующей модели транспорта</param>
public sealed record VehicleUpdateDto([property: Required, StringLength(20, MinimumLength = 3)] string LicensePlate, [property: Required, StringLength(80, MinimumLength = 1)] string Brand, [property: Range(0.1, 1000)] double LoadCapacity, [property: Range(1, int.MaxValue)] int VehicleModelId);
