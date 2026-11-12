
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Application.Contracts.Trips;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для получения рейсов водителей
/// </summary>
[ApiController]
[Route("api/drivers/{driverId:int}/trips")]
public sealed class DriverTripsController(
    IDriverService driverService,
    ITripService tripService) : ControllerBase
{
    /// <summary>
    /// Возвращает список рейсов указанного водителя
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<TripDto>>> GetByDriverId(
        int driverId,
        CancellationToken cancellationToken)
    {
        var driver = await driverService.GetByIdAsync(driverId, cancellationToken);

        if (driver is null)
        {
            return NotFound();
        }

        var trips = await tripService.GetByDriverIdAsync(driverId, cancellationToken);

        return Ok(trips);
    }
}
