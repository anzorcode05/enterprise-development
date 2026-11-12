namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Полная информация о рейсе
/// </summary>
public sealed record TripDto(
    int Id,
    int DriverId,
    string DriverName,
    int VehicleId,
    string LicensePlate,
    int ClientId,
    string ClientName,
    string Origin,
    string Destination,
    DateOnly DepartureDate,
    double CargoWeight,
    decimal Cost);