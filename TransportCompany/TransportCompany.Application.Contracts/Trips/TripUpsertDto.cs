
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Данные для создания или обновления рейса
/// </summary>
public sealed record TripUpsertDto(
    [Range(1, int.MaxValue)] int DriverId,
    [Range(1, int.MaxValue)] int VehicleId,
    [Range(1, int.MaxValue)] int ClientId,
    [Required, StringLength(120, MinimumLength = 1)] string Origin,
    [Required, StringLength(120, MinimumLength = 1)] string Destination,
    [NotInPast] DateOnly DepartureDate,
    [Range(0.01, 100000)] double CargoWeight,
    [Range(typeof(decimal), "0,01", "999999999")] decimal Cost);
