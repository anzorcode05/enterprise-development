using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.VehicleModels;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/vehicle-models")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class VehicleModelsController(IVehicleModelService service) : ApiControllerBase
{
    /// <summary>Возвращает все модели транспорта</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleModelDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VehicleModelDto>> GetAll() => Ok(service.GetAll());

    /// <summary>Возвращает модель транспорта по идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VehicleModelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VehicleModelDto> Get(int id) => service.GetById(id) is { } value ? Ok(value) : NotFound();

    /// <summary>Создаёт модель транспорта</summary>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleModelDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VehicleModelDto> Create(VehicleModelCreateDto request)
    {
        var value = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    /// <summary>Обновляет модель транспорта</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(VehicleModelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VehicleModelDto> Update(int id, VehicleModelUpdateDto request) =>
        service.Update(id, request) is { } value ? Ok(value) : NotFound();

    /// <summary>Удаляет модель транспорта</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id) => service.Delete(id) ? NoContent() : NotFound();
}
