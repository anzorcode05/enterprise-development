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
        var today = DateOnly.FromDateTime(DateTime.Today);

        return value switch
        {
            null => ValidationResult.Success,
            DateOnly date when date >= today => ValidationResult.Success,
            DateTime dateTime when DateOnly.FromDateTime(dateTime) >= today => ValidationResult.Success,
            _ => new ValidationResult(ErrorMessage)
        };
    }
}