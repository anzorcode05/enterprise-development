
using TransportCompany.Domain.Shared;

namespace TransportCompany.Domain.Entities;

/// <summary>
/// Водитель транспортной компании
/// </summary>
public sealed class Driver
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Полное имя водителя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Водительский стаж в годах
    /// </summary>
    public int Experience { get; set; }

    /// <summary>
    /// Категория водительского удостоверения
    /// </summary>
    public DrivingLicenseCategory LicenseCategory { get; set; }
}
