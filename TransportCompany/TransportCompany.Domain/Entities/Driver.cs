namespace TransportCompany.Domain.Entities;

/// <summary>
/// Водитель транспортного средства
/// </summary>
public class Driver
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО водителя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Стаж вождения в годах
    /// </summary>
    public int Experience { get; set; }

    /// <summary>
    /// Категория водительских прав
    /// </summary>
    public required string LicenseCategory { get; set; }
}