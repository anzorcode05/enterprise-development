using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Clients;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/clients")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class ClientsController(IClientService service) : ApiControllerBase
{
    /// <summary>Возвращает всех клиентов</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<ClientDto>> GetAll() => Ok(service.GetAll());

    /// <summary>Возвращает клиента по идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientDto> Get(int id) => service.GetById(id) is { } value ? Ok(value) : NotFound();

    /// <summary>Создаёт клиента</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ClientDto> Create(ClientCreateDto request)
    {
        var value = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    /// <summary>Обновляет данные клиента</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientDto> Update(int id, ClientUpdateDto request) =>
        service.Update(id, request) is { } value ? Ok(value) : NotFound();

    /// <summary>Удаляет клиента</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id) => service.Delete(id) ? NoContent() : NotFound();

    /// <summary>Возвращает клиентов, заказывавших перевозку на указанной модели транспорта</summary>
    [HttpGet("by-vehicle-model/{vehicleModelId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<ClientDto>> ByVehicleModel(int vehicleModelId) =>
        Ok(service.GetByVehicleModel(vehicleModelId));

    /// <summary>Возвращает клиентов, отсортированных по общей стоимости перевозок</summary>
    [HttpGet("analytics/top-by-cost")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientCostDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IReadOnlyList<ClientCostDto>> TopByCost([FromQuery] int count = 5) =>
        count is < 1 or > 100 ? BadRequest("Параметр count должен быть от 1 до 100.") : Ok(service.GetTopByTotalCost(count));
}
