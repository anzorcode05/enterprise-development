using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Trips;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/trips")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class TripsController(ITripService service) : ApiControllerBase
{
    /// <summary>Возвращает все рейсы</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TripDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<TripDto>> GetAll() => Ok(service.GetAll());

    /// <summary>Возвращает рейс по идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TripDto> Get(int id) => service.GetById(id) is { } value ? Ok(value) : NotFound();

    /// <summary>Создаёт рейс</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TripDto> Create(TripCreateDto request)
    {
        var value = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    /// <summary>Обновляет рейс</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TripDto> Update(int id, TripUpdateDto request) =>
        service.Update(id, request) is { } value ? Ok(value) : NotFound();

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id) => service.Delete(id) ? NoContent() : NotFound();
}
