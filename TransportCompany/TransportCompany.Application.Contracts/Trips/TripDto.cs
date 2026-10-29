namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Полная информация о рейсе.
/// </summary>
/// <param name="Id">идентификатор рейса</param>
/// <param name="DriverId">идентификатор водителя</param>
/// <param name="DriverName">ФИО водителя</param>
/// <param name="VehicleId">идентификатор транспортного средства</param>
/// <param name="LicensePlate">регистрационный номер</param>
/// <param name="ClientId">идентификатор клиента</param>
/// <param name="ClientName">имя клиента</param>
/// <param name="Origin">пункт отправления</param>
/// <param name="Destination">пункт назначения</param>
/// <param name="DepartureDate">дата отправления</param>
/// <param name="CargoWeight">масса груза</param>
/// <param name="Cost">стоимость перевозки</param>
public sealed record TripDto(int Id, int DriverId, string DriverName, int VehicleId, string LicensePlate, int ClientId, string ClientName, string Origin, string Destination, DateOnly DepartureDate, double CargoWeight, decimal Cost);
