
namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Результат аналитики по количеству рейсов водителя
/// </summary>
/// <param name="DriverId">Идентификатор водителя</param>
/// <param name="DriverName">ФИО водителя</param>
/// <param name="TripCount">Количество рейсов</param>
public sealed record DriverTripCountDto(
    int DriverId,
    string DriverName,
    int TripCount);
