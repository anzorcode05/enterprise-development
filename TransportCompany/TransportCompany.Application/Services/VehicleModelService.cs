
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с моделями транспорта
/// </summary>
public sealed class VehicleModelService(IVehicleModelRepository models)
    : CrudService<VehicleModel, VehicleModelDto, VehicleModelUpsertDto>(models),
      IVehicleModelService
{
    /// <inheritdoc/>
    protected override VehicleModelDto ToDto(VehicleModel model) =>
        new(model.Id, model.BodyType, model.BodyVolume);

    /// <inheritdoc/>
    protected override Task<VehicleModel> CreateEntityAsync(
        VehicleModelUpsertDto request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new VehicleModel
        {
            BodyType = request.BodyType,
            BodyVolume = request.BodyVolume
        });
    }

    /// <inheritdoc/>
    protected override Task UpdateEntityAsync(
        VehicleModel model,
        VehicleModelUpsertDto request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

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
        if (await Repository.GetByIdAsync(id, cancellationToken) is null)
        {
            return false;
        }

        if (await models.HasVehiclesAsync(id, cancellationToken))
        {
            throw new InvalidOperationException(
                "Нельзя удалить модель, которая используется транспортом");
        }

        return await Repository.DeleteAsync(id, cancellationToken);
    }
}
