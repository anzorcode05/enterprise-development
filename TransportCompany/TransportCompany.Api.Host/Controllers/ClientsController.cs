
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Clients;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для работы с клиентами
/// </summary>
[Route("api/clients")]
public sealed class ClientsController(IClientService service)
    : ApiControllerBase<ClientDto, ClientUpsertDto>(service)
{
    /// <summary>
    /// Возвращает идентификатор клиента
    /// </summary>
    protected override int GetId(ClientDto item) => item.Id;

    /// <summary>
    /// Возвращает клиентов, использующих указанную модель транспорта
    /// </summary>
    [HttpGet("by-vehicle-model/{vehicleModelId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> ByVehicleModel(
        int vehicleModelId,
        CancellationToken cancellationToken)
    {
        var result = await service.GetByVehicleModelAsync(
            vehicleModelId,
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает клиентов с наибольшей общей стоимостью перевозок
    /// </summary>
    [HttpGet("analytics/top-by-cost")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientCostDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<ClientCostDto>>> TopByCost(
        [FromQuery] int count = 5,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetTopByTotalCostAsync(
            count,
            cancellationToken);

        return Ok(result);
    }
}
