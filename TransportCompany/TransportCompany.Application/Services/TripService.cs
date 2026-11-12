
using TransportCompany.Application.Contracts.Trips;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с рейсами
/// </summary>
public sealed class TripService(
    IRepository<Trip> trips,
    IRepository<Driver> drivers,
    IRepository<Vehicle> vehicles,
    IRepository<Client> clients)
    : CrudService<Trip, TripDto, TripUpsertDto>(trips), ITripService
{
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    protected override async Task<Trip> CreateEntityAsync(
        TripUpsertDto request,
        int id)
    {
        var driver = await drivers.GetByIdAsync(request.DriverId)
            ?? throw new KeyNotFoundException("Водитель не найден");

        var vehicle = await vehicles.GetByIdAsync(request.VehicleId)
            ?? throw new KeyNotFoundException("Транспортное средство не найдено");

        var client = await clients.GetByIdAsync(request.ClientId)
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

    /// <inheritdoc/>
    protected override async Task UpdateEntityAsync(
        Trip trip,
        TripUpsertDto request)
    {
        var driver = await drivers.GetByIdAsync(request.DriverId)
            ?? throw new KeyNotFoundException("Водитель не найден");

        var vehicle = await vehicles.GetByIdAsync(request.VehicleId)
            ?? throw new KeyNotFoundException("Транспортное средство не найдено");

        var client = await clients.GetByIdAsync(request.ClientId)
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

    /// <summary>
    /// Возвращает рейсы указанного клиента
    /// </summary>
    public async Task<IReadOnlyList<TripDto>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tripsList = await Repository.FindAsync(
            trip => trip.Client.Id == clientId);

        return tripsList.Select(ToDto).ToList();
    }

    /// <summary>
    /// Возвращает рейсы указанного водителя
    /// </summary>
    public async Task<IReadOnlyList<TripDto>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tripsList = await Repository.FindAsync(
            trip => trip.Driver.Id == driverId);

        return tripsList.Select(ToDto).ToList();
    }
}
