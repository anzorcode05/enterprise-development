
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с моделями транспорта
/// </summary>
public sealed class VehicleModelService
    : CrudService<VehicleModel, VehicleModelDto, VehicleModelUpsertDto>,
      IVehicleModelService
{
    private readonly IRepository<Vehicle> _vehicles;

    /// <summary>
    /// Создаёт сервис моделей транспорта
    /// </summary>
    public VehicleModelService(
        IRepository<VehicleModel> models,
        IRepository<Vehicle> vehicles)
        : base(models)
    {
        _vehicles = vehicles;
    }

    /// <inheritdoc/>
    protected override VehicleModelDto ToDto(VehicleModel model) =>
        new(model.Id, model.BodyType, model.BodyVolume);

    /// <inheritdoc/>
    protected override Task<VehicleModel> CreateEntityAsync(
        VehicleModelUpsertDto request,
        int id)
    {
        return Task.FromResult(new VehicleModel
        {
            Id = id,
            BodyType = request.BodyType,
            BodyVolume = request.BodyVolume
        });
    }

    /// <inheritdoc/>
    protected override Task UpdateEntityAsync(
        VehicleModel model,
        VehicleModelUpsertDto request)
    {
        model.BodyType = request.BodyType;
        model.BodyVolume = request.BodyVolume;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Удаляет модель транспорта, если она не используется транспортными средствами
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

        var vehicles = await _vehicles.FindAsync(
            vehicle => vehicle.VehicleModel.Id == id);

        if (vehicles.Count > 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить модель, которая используется транспортом");
        }

        return await Repository.DeleteAsync(id);
    }
}
