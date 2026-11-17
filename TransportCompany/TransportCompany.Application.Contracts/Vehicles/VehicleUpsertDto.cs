
using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Данные для создания или обновления транспортного средства
/// </summary>
public sealed record VehicleUpsertDto(
    [Required, StringLength(20, MinimumLength = 3)] string LicensePlate,
    [Required, StringLength(80, MinimumLength = 1)] string Brand,
    [Range(0.1, 1000)] double LoadCapacity,
    [Range(1, int.MaxValue)] int VehicleModelId);
