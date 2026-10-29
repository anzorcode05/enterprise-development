using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Vehicles;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/vehicles")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class VehiclesController(IVehicleService service) : ApiControllerBase
{
    /// <summary>Возвращает весь транспорт</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VehicleDto>> GetAll() => Ok(service.GetAll());

    /// <summary>Возвращает транспортное средство по идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VehicleDto> Get(int id) => service.GetById(id) is { } value ? Ok(value) : NotFound();

    /// <summary>Добавляет транспортное средство</summary>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VehicleDto> Create(VehicleCreateDto request)
    {
        var value = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    /// <summary>Обновляет транспортное средство</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VehicleDto> Update(int id, VehicleUpdateDto request) =>
        service.Update(id, request) is { } value ? Ok(value) : NotFound();

    /// <summary>Удаляет транспортное средство</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id) => service.Delete(id) ? NoContent() : NotFound();

    /// <summary>Возвращает транспорт, который участвовал хотя бы в одном рейсе</summary>
    [HttpGet("on-trips")]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VehicleDto>> OnTrips() => Ok(service.GetVehiclesOnTrips());

    /// <summary>Возвращает количество рейсов для каждого транспортного средства</summary>
    [HttpGet("analytics/trip-counts")]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleTripCountDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VehicleTripCountDto>> TripCounts() => Ok(service.GetTripCounts());
}
