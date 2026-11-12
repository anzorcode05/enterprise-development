
namespace TransportCompany.Domain.Entities;

/// <summary>
/// Рейс транспортной компании
/// </summary>
public sealed class Trip
{
    /// <summary>
    /// Идентификатор рейса
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Водитель, назначенный на рейс
    /// </summary>
    public required Driver Driver { get; set; }

    /// <summary>
    /// Транспортное средство, назначенное на рейс
    /// </summary>
    public required Vehicle Vehicle { get; set; }

    /// <summary>
    /// Клиент, заказавший перевозку
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    public required string Origin { get; set; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    public required string Destination { get; set; }

    /// <summary>
    /// Дата отправления
    /// </summary>
    public DateOnly DepartureDate { get; set; }

    /// <summary>
    /// Масса перевозимого груза в тоннах
    /// </summary>
    public double CargoWeight { get; set; }

    /// <summary>
    /// Стоимость перевозки
    /// </summary>
    public decimal Cost { get; set; }
}
