using TransportCompany.Domain.Shared;

namespace TransportCompany.Domain.Entities;

/// <summary>
/// Модель транспортного средства 
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип кузова
    /// </summary>
    public BodyType BodyType { get; set; }

    /// <summary>
    /// Объём кузова в кубических метрах
    /// </summary>
    public double BodyVolume { get; set; }
}