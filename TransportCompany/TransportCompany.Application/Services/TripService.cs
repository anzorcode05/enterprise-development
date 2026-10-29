using TransportCompany.Application.Contracts.Trips;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;
namespace TransportCompany.Application.Services;
public sealed class TripService : CrudService<Trip, TripDto, TripCreateDto, TripUpdateDto>, ITripService
{
    private readonly IRepository<Driver> _drivers; private readonly IRepository<Vehicle> _vehicles; private readonly IRepository<Client> _clients;
    public TripService(IRepository<Trip> trips,IRepository<Driver> drivers,IRepository<Vehicle> vehicles,IRepository<Client> clients) : base(trips) { _drivers=drivers; _vehicles=vehicles; _clients=clients; }
    protected override TripDto ToDto(Trip t) => new(t.Id,t.Driver.Id,t.Driver.FullName,t.Vehicle.Id,t.Vehicle.LicensePlate,t.Client.Id,t.Client.Name,t.Origin,t.Destination,t.DepartureDate,t.CargoWeight,t.Cost);
    protected override Trip FromCreate(TripCreateDto r,int id) => Build(id,r.DriverId,r.VehicleId,r.ClientId,r.Origin,r.Destination,r.DepartureDate,r.CargoWeight,r.Cost);
    protected override Trip FromUpdate(Trip t,TripUpdateDto r)
    {
        var updated = Build(t.Id,r.DriverId,r.VehicleId,r.ClientId,r.Origin,r.Destination,r.DepartureDate,r.CargoWeight,r.Cost);
        t.Driver = updated.Driver;
        t.Vehicle = updated.Vehicle;
        t.Client = updated.Client;
        t.Origin = updated.Origin;
        t.Destination = updated.Destination;
        t.DepartureDate = updated.DepartureDate;
        t.CargoWeight = updated.CargoWeight;
        t.Cost = updated.Cost;
        return t;
    }
    private Trip Build(int id,int driverId,int vehicleId,int clientId,string origin,string destination,DateOnly date,double weight,decimal cost) => new()
    {
        Id=id, Driver=_drivers.GetById(driverId) ?? throw new ArgumentException("Водитель не найден."),
        Vehicle=_vehicles.GetById(vehicleId) ?? throw new ArgumentException("Транспортное средство не найдено."),
        Client=_clients.GetById(clientId) ?? throw new ArgumentException("Клиент не найден."),
        Origin=origin.Trim(), Destination=destination.Trim(), DepartureDate=date, CargoWeight=weight, Cost=cost
    };
}
