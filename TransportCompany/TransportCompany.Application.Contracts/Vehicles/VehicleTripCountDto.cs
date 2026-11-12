namespace TransportCompany.Application.Contracts.Vehicles;

/// <summary>
/// Результат аналитики по количеству рейсов транспортного средства
/// </summary>
public sealed record VehicleTripCountDto(
    int VehicleId,
    string LicensePlate,
    int TripCount);