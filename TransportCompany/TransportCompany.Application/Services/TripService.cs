
using TransportCompany.Application.Contracts.Trips;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

public sealed class TripService
    : CrudService<Trip, TripDto, TripUpsertDto>, ITripService
{
    private readonly IRepository<Driver> _drivers;
    private readonly IRepository<Vehicle> _vehicles;
    private readonly IRepository<Client> _clients;

    public TripService(
        IRepository<Trip> trips,
        IRepository<Driver> drivers,
        IRepository<Vehicle> vehicles,
        IRepository<Client> clients)
        : base(trips)
    {
        _drivers = drivers;
        _vehicles = vehicles;
        _clients = clients;
    }

    protected override TripDto ToDto(Trip trip)
    {
        return new TripDto(
            trip.Id,
            trip.Driver.Id,
            trip.Driver.FullName,
            trip.Vehicle.Id,
            trip.Vehicle.LicensePlate,
            trip.Client.Id,
            trip.Client.Name,
            trip.Origin,
            trip.Destination,
            trip.DepartureDate,
            trip.CargoWeight,
            trip.Cost);
    }

    protected override async Task<Trip> CreateEntityAsync(
        TripUpsertDto request,
        int id)
    {
        var driver = await _drivers.GetByIdAsync(request.DriverId)
            ?? throw new KeyNotFoundException("Водитель не найден");

        var vehicle = await _vehicles.GetByIdAsync(request.VehicleId)
            ?? throw new KeyNotFoundException("Транспортное средство не найдено");

        var client = await _clients.GetByIdAsync(request.ClientId)
            ?? throw new KeyNotFoundException("Клиент не найден");

        return new Trip
        {
            Id = id,
            Driver = driver,
            Vehicle = vehicle,
            Client = client,
            Origin = request.Origin.Trim(),
            Destination = request.Destination.Trim(),
            DepartureDate = request.DepartureDate,
            CargoWeight = request.CargoWeight,
            Cost = request.Cost
        };
    }

    protected override async Task UpdateEntityAsync(
        Trip trip,
        TripUpsertDto request)
    {
        var driver = await _drivers.GetByIdAsync(request.DriverId)
            ?? throw new KeyNotFoundException("Водитель не найден");

        var vehicle = await _vehicles.GetByIdAsync(request.VehicleId)
            ?? throw new KeyNotFoundException("Транспортное средство не найдено");

        var client = await _clients.GetByIdAsync(request.ClientId)
            ?? throw new KeyNotFoundException("Клиент не найден");

        trip.Driver = driver;
        trip.Vehicle = vehicle;
        trip.Client = client;
        trip.Origin = request.Origin.Trim();
        trip.Destination = request.Destination.Trim();
        trip.DepartureDate = request.DepartureDate;
        trip.CargoWeight = request.CargoWeight;
        trip.Cost = request.Cost;
    }


    public async Task<IReadOnlyList<TripDto>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var trips = await Repository.FindAsync(
            trip => trip.Client.Id == clientId);

        return trips.Select(ToDto).ToList();
    }


    public async Task<IReadOnlyList<TripDto>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var trips = await Repository.FindAsync(
            trip => trip.Driver.Id == driverId);

        return trips.Select(ToDto).ToList();
    }

    private Task<IReadOnlyList<Trip>> _tripsByClient(int clientId)
    {
        return Repository.FindAsync(trip => trip.Client.Id == clientId);
    }
}
