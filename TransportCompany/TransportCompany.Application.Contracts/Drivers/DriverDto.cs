namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Данные водителя, возвращаемые API.
/// </summary>
/// <param name="Id">идентификатор водителя</param>
/// <param name="PassportNumber">номер паспорта</param>
/// <param name="FullName">ФИО водителя</param>
/// <param name="Experience">стаж в годах</param>
/// <param name="LicenseCategory">категория водительского удостоверения</param>
public sealed record DriverDto(int Id, string PassportNumber, string FullName, int Experience, string LicenseCategory);
