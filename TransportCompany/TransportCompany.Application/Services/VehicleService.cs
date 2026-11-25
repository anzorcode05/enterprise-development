
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с транспортными средствами
/// </summary>
public sealed class VehicleService(
    IVehicleRepository vehicles,
    IRepository<VehicleModel> models)
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
        CancellationToken cancellationToken)
    {
        var model = await models.GetByIdAsync(
            request.VehicleModelId,
            cancellationToken)
            ?? throw new KeyNotFoundException("Модель транспорта не найдена");

        return new Vehicle
        {
            LicensePlate = request.LicensePlate.Trim(),
            Brand = request.Brand.Trim(),
            LoadCapacity = request.LoadCapacity,
            VehicleModel = model
        };
    }

    /// <inheritdoc/>
    protected override async Task UpdateEntityAsync(
        Vehicle vehicle,
        VehicleUpsertDto request,
        CancellationToken cancellationToken)
    {
        var model = await models.GetByIdAsync(
            request.VehicleModelId,
            cancellationToken)
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
        var vehiclesList = await vehicles.GetVehiclesOnTripsAsync(
            cancellationToken);

        return vehiclesList.Select(ToDto).ToList();
    }

    /// <summary>
    /// Возвращает количество рейсов для каждого транспортного средства
    /// </summary>
    public async Task<IReadOnlyList<VehicleTripCountDto>> GetTripCountsAsync(
        CancellationToken cancellationToken = default)
    {
        var summaries = await vehicles.GetTripCountsAsync(cancellationToken);

        return summaries
            .Select(item => new VehicleTripCountDto(
                item.VehicleId,
                item.LicensePlate,
                item.TripCount))
            .ToList();
    }

    /// <summary>
    /// Удаляет транспортное средство, если оно не используется в рейсах
    /// </summary>
    public override async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (await Repository.GetByIdAsync(id, cancellationToken) is null)
        {
            return false;
        }

        if (await vehicles.HasTripsAsync(id, cancellationToken))
        {
            throw new InvalidOperationException(
                "Нельзя удалить транспортное средство, которое используется в рейсах");
        }

        return await Repository.DeleteAsync(id, cancellationToken);
    }
}
