using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Данные водителя, возвращаемые API
/// </summary>
public sealed record DriverDto(
    int Id,
    string LicenseNumber,
    string FullName,
    int Experience,
    DrivingLicenseCategory LicenseCategory);