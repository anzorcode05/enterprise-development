using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Данные для создания водителя.
/// </summary>
/// <param name="PassportNumber">номер паспорта</param>
/// <param name="FullName">ФИО водителя</param>
/// <param name="Experience">стаж в годах</param>
/// <param name="LicenseCategory">категория водительского удостоверения</param>
public sealed record DriverCreateDto([property: Required, StringLength(20, MinimumLength = 3)] string PassportNumber, [property: Required, StringLength(150, MinimumLength = 1)] string FullName, [property: Range(0, 70)] int Experience, [property: Required, StringLength(5, MinimumLength = 1)] string LicenseCategory);
