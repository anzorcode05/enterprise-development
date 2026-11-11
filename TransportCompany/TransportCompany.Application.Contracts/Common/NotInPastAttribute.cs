
using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Common;

/// <summary>
/// Проверяет, что дата не находится в прошлом
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class NotInPastAttribute : ValidationAttribute
{
    public NotInPastAttribute()
        : base("Дата не может быть в прошлом.")
    {
    }

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is not DateOnly date)
        {
            return ValidationResult.Success;
        }

        return date < DateOnly.FromDateTime(DateTime.Today)
            ? new ValidationResult(ErrorMessage)
            : ValidationResult.Success;
    }
}
