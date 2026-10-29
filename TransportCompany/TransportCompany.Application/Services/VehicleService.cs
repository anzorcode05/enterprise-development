using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;
namespace TransportCompany.Application.Services;
public sealed class VehicleService : CrudService<Vehicle, VehicleDto, VehicleCreateDto, VehicleUpdateDto>, IVehicleService
{
    private readonly IRepository<VehicleModel> _models;
    private readonly IRepository<Trip> _trips;
    public VehicleService(IRepository<Vehicle> vehicles, IRepository<VehicleModel> models, IRepository<Trip> trips) : base(vehicles) { _models=models; _trips=trips; }
    protected override VehicleDto ToDto(Vehicle v) => new(v.Id,v.LicensePlate,v.Brand,v.LoadCapacity,v.VehicleModel.Id,v.VehicleModel.BodyType.ToString(),v.VehicleModel.BodyVolume);
    protected override Vehicle FromCreate(VehicleCreateDto r,int id) => new() { Id=id, LicensePlate=r.LicensePlate.Trim(), Brand=r.Brand.Trim(), LoadCapacity=r.LoadCapacity, VehicleModel=_models.GetById(r.VehicleModelId) ?? throw new ArgumentException("Модель транспорта не найдена.") };
    protected override Vehicle FromUpdate(Vehicle v,VehicleUpdateDto r)
    {
        v.LicensePlate = r.LicensePlate.Trim();
        v.Brand = r.Brand.Trim();
        v.LoadCapacity = r.LoadCapacity;
        v.VehicleModel = _models.GetById(r.VehicleModelId) ?? throw new ArgumentException("Модель транспорта не найдена.");
        return v;
    }
    public IReadOnlyList<VehicleDto> GetVehiclesOnTrips() => _trips.GetAll().Select(t=>t.Vehicle).DistinctBy(v=>v.Id).OrderBy(v=>v.Id).Select(ToDto).ToList();
    public IReadOnlyList<VehicleTripCountDto> GetTripCounts()
    {
        var tripCounts = _trips.GetAll()
            .GroupBy(trip => trip.Vehicle.Id)
            .ToDictionary(group => group.Key, group => group.Count());

        return Repository.GetAll()
            .OrderBy(vehicle => vehicle.Id)
            .Select(vehicle => new VehicleTripCountDto(
                vehicle.Id,
                vehicle.LicensePlate,
                tripCounts.GetValueOrDefault(vehicle.Id)))
            .ToList();
    }
}
