
using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Данные для создания или обновления водителя
/// </summary>
/// <param name="LicenseNumber">Десятизначный номер водительского удостоверения</param>
/// <param name="FullName">ФИО водителя</param>
/// <param name="Experience">Стаж в годах</param>
/// <param name="LicenseCategory">Категория водительского удостоверения</param>
public sealed record DriverUpsertDto(
    [property: Required, RegularExpression(@"^\d{10}$")] string LicenseNumber,
    [property: Required, StringLength(150, MinimumLength = 1)] string FullName,
    [property: Range(0, 70)] int Experience,
    DrivingLicenseCategory LicenseCategory);
