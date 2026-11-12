using System.ComponentModel.DataAnnotations;
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Данные для создания или обновления рейса
/// </summary>
public sealed record TripUpsertDto(
    [property: Range(1, int.MaxValue)] int DriverId,
    [property: Range(1, int.MaxValue)] int VehicleId,
    [property: Range(1, int.MaxValue)] int ClientId,
    [property: Required, StringLength(120, MinimumLength = 1)] string Origin,
    [property: Required, StringLength(120, MinimumLength = 1)] string Destination,
    [property: NotInPast] DateOnly DepartureDate,
    [property: Range(0.01, 100000)] double CargoWeight,
    [property: Range(typeof(decimal), "0.01", "999999999")] decimal Cost);