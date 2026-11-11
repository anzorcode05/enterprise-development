
using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.VehicleModels;

/// <summary>
/// Данные для создания или обновления модели транспортного средства
/// </summary>
/// <param name="BodyType">Тип кузова</param>
/// <param name="BodyVolume">Объём кузова</param>
public sealed record VehicleModelUpsertDto(
    BodyType BodyType,
    [property: Range(0.01, 10000)] double BodyVolume);
