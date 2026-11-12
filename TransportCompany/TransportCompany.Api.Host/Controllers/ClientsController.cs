
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Clients;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/clients")]
public sealed class ClientsController(IClientService service)
    : ApiControllerBase<ClientDto, ClientUpsertDto>(service)
{
    protected override int GetId(ClientDto item) => item.Id;

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

    [HttpGet("analytics/top-by-cost")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientCostDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<ClientCostDto>>> TopByCost(
        [FromQuery] int count = 5,
        CancellationToken cancellationToken = default)
    {
        if (count is < 1 or > 100)
        {
            return BadRequest("Параметр count должен быть от 1 до 100");
        }

        var result = await service.GetTopByTotalCostAsync(
            count,
            cancellationToken);

        return Ok(result);
    }
}
