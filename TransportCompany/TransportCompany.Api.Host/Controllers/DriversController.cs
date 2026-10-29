using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Drivers;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/drivers")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class DriversController(IDriverService service) : ApiControllerBase
{
    /// <summary>Возвращает всех водителей</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<DriverDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<DriverDto>> GetAll() => Ok(service.GetAll());

    /// <summary>Возвращает водителя по идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DriverDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DriverDto> Get(int id) => service.GetById(id) is { } value ? Ok(value) : NotFound();

    /// <summary>Создаёт водителя</summary>
    [HttpPost]
    [ProducesResponseType(typeof(DriverDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DriverDto> Create(DriverCreateDto request)
    {
        var value = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    /// <summary>Обновляет данные водителя</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DriverDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DriverDto> Update(int id, DriverUpdateDto request) =>
        service.Update(id, request) is { } value ? Ok(value) : NotFound();

    /// <summary>Удаляет водителя</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id) => service.Delete(id) ? NoContent() : NotFound();

    /// <summary>Возвращает топ водителей по количеству рейсов</summary>
    [HttpGet("analytics/top-by-trip-count")]
    [ProducesResponseType(typeof(IReadOnlyList<DriverTripCountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IReadOnlyList<DriverTripCountDto>> TopByTrips([FromQuery] int count = 5) =>
        count is < 1 or > 100 ? BadRequest("Параметр count должен быть от 1 до 100.") : Ok(service.GetTopByTripCount(count));
}
