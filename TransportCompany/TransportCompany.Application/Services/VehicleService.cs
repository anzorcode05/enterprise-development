
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

public sealed class VehicleService
    : CrudService<Vehicle, VehicleDto, VehicleUpsertDto>, IVehicleService
{
    private readonly IRepository<VehicleModel> _models;
    private readonly IRepository<Trip> _trips;

    public VehicleService(
        IRepository<Vehicle> vehicles,
        IRepository<VehicleModel> models,
        IRepository<Trip> trips)
        : base(vehicles)
    {
        _models = models;
        _trips = trips;
    }

    protected override VehicleDto ToDto(Vehicle vehicle)
    {
        var model = vehicle.VehicleModel;

        return new VehicleDto(
            vehicle.Id,
            vehicle.LicensePlate,
            vehicle.Brand,
            vehicle.LoadCapacity,
            new VehicleModelDto(
                model.Id,
                model.BodyType,
                model.BodyVolume));
    }

    protected override async Task<Vehicle> CreateEntityAsync(
        VehicleUpsertDto request,
        int id)
    {
        var model = await _models.GetByIdAsync(request.VehicleModelId)
            ?? throw new KeyNotFoundException("Модель транспорта не найдена");

        return new Vehicle
        {
            Id = id,
            LicensePlate = request.LicensePlate.Trim(),
            Brand = request.Brand.Trim(),
            LoadCapacity = request.LoadCapacity,
            VehicleModel = model
        };
    }

    protected override async Task UpdateEntityAsync(
        Vehicle vehicle,
        VehicleUpsertDto request)
    {
        var model = await _models.GetByIdAsync(request.VehicleModelId)
            ?? throw new KeyNotFoundException("Модель транспорта не найдена");

        vehicle.LicensePlate = request.LicensePlate.Trim();
        vehicle.Brand = request.Brand.Trim();
        vehicle.LoadCapacity = request.LoadCapacity;
        vehicle.VehicleModel = model;
    }

    public async Task<IReadOnlyList<VehicleDto>> GetVehiclesOnTripsAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var trips = await _trips.GetAllAsync();

        return trips
            .Select(trip => trip.Vehicle)
            .GroupBy(vehicle => vehicle.Id)
            .Select(group => group.First())
            .OrderBy(vehicle => vehicle.Id)
            .Select(ToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<VehicleTripCountDto>> GetTripCountsAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var trips = await _trips.GetAllAsync();
        var vehicles = await Repository.GetAllAsync();

        var tripCounts = trips
            .GroupBy(trip => trip.Vehicle.Id)
            .ToDictionary(group => group.Key, group => group.Count());

        return vehicles
            .OrderBy(vehicle => vehicle.Id)
            .Select(vehicle => new VehicleTripCountDto(
                vehicle.Id,
                vehicle.LicensePlate,
                tripCounts.GetValueOrDefault(vehicle.Id)))
            .ToList();
    }

    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var vehicle = await Repository.GetByIdAsync(id);

        if (vehicle is null)
        {
            return false;
        }

        var trips = await _trips.FindAsync(
            trip => trip.Vehicle.Id == id);

        if (trips.Count > 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить транспортное средство, которое используется в рейсах");
        }

        return await Repository.DeleteAsync(id);
    }
}
