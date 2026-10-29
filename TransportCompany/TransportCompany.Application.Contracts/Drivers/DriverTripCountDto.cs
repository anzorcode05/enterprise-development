namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Результат аналитики по количеству рейсов водителя.
/// </summary>
/// <param name="DriverId">идентификатор водителя</param>
/// <param name="DriverName">ФИО водителя</param>
/// <param name="TripCount">количество выполненных рейсов</param>
public sealed record DriverTripCountDto(int DriverId, string DriverName, int TripCount);
