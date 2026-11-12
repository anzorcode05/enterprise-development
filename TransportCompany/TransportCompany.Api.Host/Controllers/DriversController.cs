
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Drivers;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для работы с водителями
/// </summary>
[Route("api/drivers")]
public sealed class DriversController(IDriverService service)
    : ApiControllerBase<DriverDto, DriverUpsertDto>(service)
{
    /// <summary>
    /// Возвращает идентификатор водителя
    /// </summary>
    protected override int GetId(DriverDto item) => item.Id;

    /// <summary>
    /// Возвращает водителей с наибольшим количеством рейсов
    /// </summary>
    [HttpGet("analytics/top-by-trip-count")]
    [ProducesResponseType(typeof(IReadOnlyList<DriverTripCountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<DriverTripCountDto>>> TopByTrips(
        [FromQuery] int count = 5,
        CancellationToken cancellationToken = default)
    {
        if (count is < 1 or > 100)
        {
            return BadRequest("Параметр count должен быть от 1 до 100");
        }

        var result = await service.GetTopByTripCountAsync(
            count,
            cancellationToken);

        return Ok(result);
    }
}