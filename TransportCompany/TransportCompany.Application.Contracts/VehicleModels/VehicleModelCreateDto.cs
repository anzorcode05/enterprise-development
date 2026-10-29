using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.VehicleModels;

/// <summary>
/// Данные для создания модели транспорта.
/// </summary>
/// <param name="BodyType">допустимое название типа кузова</param>
/// <param name="BodyVolume">положительный объём кузова</param>
public sealed record VehicleModelCreateDto([property: Required, StringLength(30, MinimumLength = 1)] string BodyType, [property: Range(0.01, 10000)] double BodyVolume);
