namespace TransportCompany.Domain.ReadModels;

/// <summary>
/// Сводка по количеству рейсов водителя
/// </summary>
public sealed record DriverTripCountSummary(
    int DriverId,
    string DriverName,
    int TripCount);