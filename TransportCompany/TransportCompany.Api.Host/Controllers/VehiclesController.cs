
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Vehicles;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для работы с транспортом
/// </summary>
[Route("api/vehicles")]
public sealed class VehiclesController(IVehicleService service)
    : ApiControllerBase<VehicleDto, VehicleUpsertDto>(service)
{
    /// <summary>
    /// Возвращает идентификатор транспортного средства
    /// </summary>
    protected override int GetId(VehicleDto item) => item.Id;

    /// <summary>
    /// Возвращает транспортные средства, участвующие в рейсах
    /// </summary>
    [HttpGet("on-trips")]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VehicleDto>>> OnTrips(
        CancellationToken cancellationToken)
    {
        var result = await service.GetVehiclesOnTripsAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает количество рейсов для каждого транспортного средства
    /// </summary>
    [HttpGet("analytics/trip-counts")]
    [ProducesResponseType(typeof(IReadOnlyList<VehicleTripCountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VehicleTripCountDto>>> TripCounts(
        CancellationToken cancellationToken)
    {
        var result = await service.GetTripCountsAsync(cancellationToken);
        return Ok(result);
    }
}
