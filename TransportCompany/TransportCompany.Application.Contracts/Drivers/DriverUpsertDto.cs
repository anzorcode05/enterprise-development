
using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Shared;

namespace TransportCompany.Application.Contracts.Drivers;

/// <summary>
/// Данные для создания или обновления водителя
/// </summary>
public sealed record DriverUpsertDto(
    [Required, RegularExpression(@"^\d{10}$")] string LicenseNumber,
    [Required, StringLength(150, MinimumLength = 1)] string FullName,
    [Range(0, 70)] int Experience,
    DrivingLicenseCategory LicenseCategory);
