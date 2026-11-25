namespace TransportCompany.Domain.ReadModels;

/// <summary>
/// Сводка по количеству рейсов транспортного средства
/// </summary>
public sealed record VehicleTripCountSummary(
    int VehicleId,
    string LicensePlate,
    int TripCount);