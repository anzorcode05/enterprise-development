
using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Данные для создания или обновления транспортного средства
/// </summary>
/// <param name="LicensePlate">Регистрационный номер</param>
/// <param name="Brand">Марка транспорта</param>
/// <param name="LoadCapacity">Грузоподъёмность</param>
/// <param name="VehicleModelId">Идентификатор модели транспорта</param>
public sealed record VehicleUpsertDto(
    [property: Required, StringLength(20, MinimumLength = 3)] string LicensePlate,
    [property: Required, StringLength(80, MinimumLength = 1)] string Brand,
    [property: Range(0.1, 1000)] double LoadCapacity,
    [property: Range(1, int.MaxValue)] int VehicleModelId);
