
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с водителями
/// </summary>
public sealed class DriverService(
    IRepository<Driver> drivers,
    IRepository<Trip> trips)
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
    protected override Task<Driver> CreateEntityAsync(DriverUpsertDto request, int id)
    {
        return Task.FromResult(new Driver
        {
            Id = id,
            LicenseNumber = request.LicenseNumber.Trim(),
            FullName = request.FullName.Trim(),
            Experience = request.Experience,
            LicenseCategory = request.LicenseCategory
        });
    }

    /// <inheritdoc/>
    protected override Task UpdateEntityAsync(Driver driver, DriverUpsertDto request)
    {
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
        cancellationToken.ThrowIfCancellationRequested();

        if (count is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                "Количество водителей должно быть от 1 до 100");
        }

        var tripsList = await trips.GetAllAsync();

        return tripsList
            .GroupBy(trip => trip.Driver.Id)
            .Select(group =>
            {
                var driver = group.First().Driver;

                return new DriverTripCountDto(
                    driver.Id,
                    driver.FullName,
                    group.Count());
            })
            .OrderByDescending(item => item.TripCount)
            .ThenBy(item => item.DriverId)
            .Take(count)
            .ToList();
    }

    /// <summary>
    /// Удаляет водителя, если он не назначен на рейсы
    /// </summary>
    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await Repository.GetByIdAsync(id) is null)
        {
            return false;
        }

        var relatedTrips = await trips.FindAsync(
            trip => trip.Driver.Id == id);

        if (relatedTrips.Count > 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить водителя, который назначен на рейсы");
        }

        return await Repository.DeleteAsync(id);
    }
}
