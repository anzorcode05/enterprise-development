namespace TransportCompany.Domain.Entities;

/// <summary>
/// Транспортное средство
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Идентификатор транспортного средства
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка транспортного средства
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Грузоподъёмность в тоннах
    /// </summary>
    public double LoadCapacity { get; set; }

    /// <summary>
    /// Модель транспортного средства
    /// </summary>
    public required VehicleModel VehicleModel { get; set; }
}