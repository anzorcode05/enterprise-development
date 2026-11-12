
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Vehicles;

namespace TransportCompany.Api.Host.Controllers;

[Route("api/vehicles")]
public sealed class VehiclesController(IVehicleService service)
    : ApiControllerBase<VehicleDto, VehicleUpsertDto>(service)
{
    protected override int GetId(VehicleDto item) => item.Id;

    [HttpGet("on-trips")]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VehicleDto>>> OnTrips(
        CancellationToken cancellationToken)
    {
        var result = await service.GetVehiclesOnTripsAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("analytics/trip-counts")]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleTripCountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VehicleTripCountDto>>> TripCounts(
        CancellationToken cancellationToken)
    {
        var result = await service.GetTripCountsAsync(cancellationToken);
        return Ok(result);
    }
}
