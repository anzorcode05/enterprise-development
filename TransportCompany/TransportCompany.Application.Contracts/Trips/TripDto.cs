
namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Полная информация о рейсе
/// </summary>
/// <param name="Id">Идентификатор рейса</param>
/// <param name="DriverId">Идентификатор водителя</param>
/// <param name="DriverName">ФИО водителя</param>
/// <param name="VehicleId">Идентификатор транспорта</param>
/// <param name="LicensePlate">Регистрационный номер</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientName">Имя клиента</param>
/// <param name="Origin">Пункт отправления</param>
/// <param name="Destination">Пункт назначения</param>
/// <param name="DepartureDate">Дата отправления</param>
/// <param name="CargoWeight">Масса груза</param>
/// <param name="Cost">Стоимость перевозки</param>
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
