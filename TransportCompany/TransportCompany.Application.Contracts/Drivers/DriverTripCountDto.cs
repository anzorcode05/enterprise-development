namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Результат аналитики по количеству рейсов водителя
/// </summary>
public sealed record DriverTripCountDto(
    int DriverId,
    string DriverName,
    int TripCount);