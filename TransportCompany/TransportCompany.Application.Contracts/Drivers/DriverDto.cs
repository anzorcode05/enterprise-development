
using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Данные водителя, возвращаемые API
/// </summary>
/// <param name="Id">Идентификатор водителя</param>
/// <param name="LicenseNumber">Номер водительского удостоверения</param>
/// <param name="FullName">ФИО водителя</param>
/// <param name="Experience">Стаж в годах</param>
/// <param name="LicenseCategory">Категория водительского удостоверения</param>
public sealed record DriverDto(
    int Id,
    string LicenseNumber,
    string FullName,
    int Experience,
    DrivingLicenseCategory LicenseCategory);
