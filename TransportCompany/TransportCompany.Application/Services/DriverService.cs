
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

public sealed class DriverService
    : CrudService<Driver, DriverDto, DriverUpsertDto>, IDriverService
{
    private readonly IRepository<Trip> _trips;

    public DriverService(IRepository<Driver> drivers, IRepository<Trip> trips)
        : base(drivers)
    {
        _trips = trips;
    }

    protected override DriverDto ToDto(Driver driver) =>
        new(
            driver.Id,
            driver.LicenseNumber,
            driver.FullName,
            driver.Experience,
            driver.LicenseCategory);

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

    protected override Task UpdateEntityAsync(Driver driver, DriverUpsertDto request)
    {
        driver.LicenseNumber = request.LicenseNumber.Trim();
        driver.FullName = request.FullName.Trim();
        driver.Experience = request.Experience;
        driver.LicenseCategory = request.LicenseCategory;

        return Task.CompletedTask;
    }

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

        var trips = await _trips.GetAllAsync();

        return trips
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

    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await Repository.GetByIdAsync(id) is null)
        {
            return false;
        }

        var trips = await _trips.FindAsync(trip => trip.Driver.Id == id);

        if (trips.Count > 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить водителя, который назначен на рейсы");
        }

        return await Repository.DeleteAsync(id);
    }
}
