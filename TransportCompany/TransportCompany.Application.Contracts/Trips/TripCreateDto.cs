using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Trips;

/// <summary>
/// Данные для создания рейса. Идентификатор рейса назначает сервер.
/// </summary>
/// <param name="DriverId">идентификатор существующего водителя</param>
/// <param name="VehicleId">идентификатор существующего транспорта</param>
/// <param name="ClientId">идентификатор существующего клиента</param>
/// <param name="Origin">пункт отправления</param>
/// <param name="Destination">пункт назначения</param>
/// <param name="DepartureDate">дата отправления</param>
/// <param name="CargoWeight">масса груза в допустимых пределах</param>
/// <param name="Cost">положительная стоимость перевозки</param>
public sealed record TripCreateDto([property: Range(1, int.MaxValue)] int DriverId, [property: Range(1, int.MaxValue)] int VehicleId, [property: Range(1, int.MaxValue)] int ClientId, [property: Required, StringLength(120, MinimumLength = 1)] string Origin, [property: Required, StringLength(120, MinimumLength = 1)] string Destination, DateOnly DepartureDate, [property: Range(0.01, 100000)] double CargoWeight, [property: Range(typeof(decimal), "0.01", "999999999")] decimal Cost);
