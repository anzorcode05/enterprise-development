using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;
namespace TransportCompany.Application.Services;
public sealed class DriverService : CrudService<Driver, DriverDto, DriverCreateDto, DriverUpdateDto>, IDriverService
{
    private readonly IRepository<Trip> _trips;
    public DriverService(IRepository<Driver> drivers, IRepository<Trip> trips) : base(drivers) => _trips = trips;
    protected override DriverDto ToDto(Driver d) => new(d.Id,d.PassportNumber,d.FullName,d.Experience,d.LicenseCategory);
    protected override Driver FromCreate(DriverCreateDto r,int id) => new() { Id=id, PassportNumber=r.PassportNumber.Trim(), FullName=r.FullName.Trim(), Experience=r.Experience, LicenseCategory=r.LicenseCategory.Trim() };
    protected override Driver FromUpdate(Driver d,DriverUpdateDto r)
    {
        d.PassportNumber = r.PassportNumber.Trim();
        d.FullName = r.FullName.Trim();
        d.Experience = r.Experience;
        d.LicenseCategory = r.LicenseCategory.Trim();
        return d;
    }
    public IReadOnlyList<DriverTripCountDto> GetTopByTripCount(int count = 5)
    {
        if (count is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Количество водителей должно быть от 1 до 100.");
        }

        return _trips.GetAll()
            .GroupBy(trip => trip.Driver)
            .Select(group => new DriverTripCountDto(group.Key.Id, group.Key.FullName, group.Count()))
            .OrderByDescending(item => item.TripCount)
            .ThenBy(item => item.DriverId)
            .Take(count)
            .ToList();
    }
}
