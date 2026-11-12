
using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Common;

/// <summary>
/// Проверяет, что указанная дата не находится в прошлом
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class NotInPastAttribute : ValidationAttribute
{
    /// <summary>
    /// Создаёт атрибут проверки даты
    /// </summary>
    public NotInPastAttribute()
    {
        ErrorMessage = "Дата не может находиться в прошлом";
    }

    /// <summary>
    /// Проверяет допустимость указанного значения
    /// </summary>
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is not DateTime date)
        {
            return new ValidationResult(ErrorMessage);
        }

        return date.Date >= DateTime.Today
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}
