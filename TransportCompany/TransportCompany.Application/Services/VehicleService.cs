
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с транспортными средствами
/// </summary>
public sealed class VehicleService(
    IRepository<Vehicle> vehicles,
    IRepository<VehicleModel> models,
    IRepository<Trip> trips)
    : CrudService<Vehicle, VehicleDto, VehicleUpsertDto>(vehicles), IVehicleService
{
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    protected override async Task<Vehicle> CreateEntityAsync(
        VehicleUpsertDto request,
        int id)
    {
        var model = await models.GetByIdAsync(request.VehicleModelId)
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

    /// <inheritdoc/>
    protected override async Task UpdateEntityAsync(
        Vehicle vehicle,
        VehicleUpsertDto request)
    {
        var model = await models.GetByIdAsync(request.VehicleModelId)
            ?? throw new KeyNotFoundException("Модель транспорта не найдена");

        vehicle.LicensePlate = request.LicensePlate.Trim();
        vehicle.Brand = request.Brand.Trim();
        vehicle.LoadCapacity = request.LoadCapacity;
        vehicle.VehicleModel = model;
    }

    /// <summary>
    /// Возвращает транспортные средства, участвовавшие в рейсах
    /// </summary>
    public async Task<IReadOnlyList<VehicleDto>> GetVehiclesOnTripsAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tripsList = await trips.GetAllAsync();

        return tripsList
            .Select(trip => trip.Vehicle)
            .GroupBy(vehicle => vehicle.Id)
            .Select(group => group.First())
            .OrderBy(vehicle => vehicle.Id)
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Возвращает количество рейсов для каждого транспортного средства
    /// </summary>
    public async Task<IReadOnlyList<VehicleTripCountDto>> GetTripCountsAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tripsList = await trips.GetAllAsync();
        var vehiclesList = await Repository.GetAllAsync();

        var tripCounts = tripsList
            .GroupBy(trip => trip.Vehicle.Id)
            .ToDictionary(group => group.Key, group => group.Count());

        return vehiclesList
            .OrderBy(vehicle => vehicle.Id)
            .Select(vehicle => new VehicleTripCountDto(
                vehicle.Id,
                vehicle.LicensePlate,
                tripCounts.GetValueOrDefault(vehicle.Id)))
            .ToList();
    }

    /// <summary>
    /// Удаляет транспортное средство, если оно не используется в рейсах
    /// </summary>
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

        var relatedTrips = await trips.FindAsync(
            trip => trip.Vehicle.Id == id);

        if (relatedTrips.Count > 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить транспортное средство, которое используется в рейсах");
        }

        return await Repository.DeleteAsync(id);
    }
}
