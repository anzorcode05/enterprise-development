
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с водителями
/// </summary>
public sealed class DriverService(IDriverRepository drivers)
    : CrudService<Driver, DriverDto, DriverUpsertDto>(drivers), IDriverService
{
    /// <inheritdoc/>
    protected override DriverDto ToDto(Driver driver) =>
        new(
            driver.Id,
            driver.LicenseNumber,
            driver.FullName,
            driver.Experience,
            driver.LicenseCategory);

    /// <inheritdoc/>
    protected override Task<Driver> CreateEntityAsync(
        DriverUpsertDto request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new Driver
        {
            LicenseNumber = request.LicenseNumber.Trim(),
            FullName = request.FullName.Trim(),
            Experience = request.Experience,
            LicenseCategory = request.LicenseCategory
        });
    }

    /// <inheritdoc/>
    protected override Task UpdateEntityAsync(
        Driver driver,
        DriverUpsertDto request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        driver.LicenseNumber = request.LicenseNumber.Trim();
        driver.FullName = request.FullName.Trim();
        driver.Experience = request.Experience;
        driver.LicenseCategory = request.LicenseCategory;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Возвращает водителей с наибольшим количеством рейсов
    /// </summary>
    public async Task<IReadOnlyList<DriverTripCountDto>> GetTopByTripCountAsync(
        int count = 5,
        CancellationToken cancellationToken = default)
    {
        if (count is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                "Количество водителей должно быть от 1 до 100");
        }

        var summaries = await drivers.GetTopByTripCountAsync(
            count,
            cancellationToken);

        return summaries
            .Select(item => new DriverTripCountDto(
                item.DriverId,
                item.DriverName,
                item.TripCount))
            .ToList();
    }

    /// <summary>
    /// Удаляет водителя, если он не назначен на рейсы
    /// </summary>
    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (await Repository.GetByIdAsync(id, cancellationToken) is null)
        {
            return false;
        }

        if (await drivers.HasTripsAsync(id, cancellationToken))
        {
            throw new InvalidOperationException(
                "Нельзя удалить водителя, который назначен на рейсы");
        }

        return await Repository.DeleteAsync(id, cancellationToken);
    }
}
