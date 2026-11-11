
namespace TransportCompany.Domain.Entities;

/// <summary>Транспортное средство транспортной компании.</summary>
public sealed class Vehicle
{
    /// <summary>Уникальный идентификатор транспортного средства.</summary>
    public required int Id { get; set; }

    /// <summary>Государственный регистрационный номер.</summary>
    public required string LicensePlate { get; set; }

    /// <summary>Марка транспортного средства.</summary>
    public required string Brand { get; set; }

    /// <summary>Максимальная грузоподъёмность в тоннах.</summary>
    public double LoadCapacity { get; set; }

    /// <summary>Модель транспортного средства.</summary>
    public required VehicleModel VehicleModel { get; set; }
}
