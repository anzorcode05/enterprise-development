
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.VehicleModels;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для работы с моделями транспорта
/// </summary>
[Route("api/[controller]")]
public sealed class VehicleModelsController(
    IVehicleModelService service)
    : ApiControllerBase<VehicleModelDto, VehicleModelUpsertDto>(service)
{
    /// <summary>
    /// Возвращает идентификатор модели транспорта
    /// </summary>
    protected override int GetId(VehicleModelDto item)
    {
        return item.Id;
    }
}
