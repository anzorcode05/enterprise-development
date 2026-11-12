
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Trips;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для работы с рейсами
/// </summary>
[Route("api/trips")]
public sealed class TripsController(ITripService service)
    : ApiControllerBase<TripDto, TripUpsertDto>(service)
{
    /// <summary>
    /// Возвращает идентификатор рейса
    /// </summary>
    protected override int GetId(TripDto item) => item.Id;

    /// <summary>
    /// Возвращает рейсы указанного клиента
    /// </summary>
    [HttpGet("by-client/{clientId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<TripDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TripDto>>> ByClient(
        int clientId,
        CancellationToken cancellationToken)
    {
        var result = await service.GetByClientIdAsync(
            clientId,
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает рейсы указанного водителя
    /// </summary>
    [HttpGet("by-driver/{driverId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<TripDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TripDto>>> ByDriver(
        int driverId,
        CancellationToken cancellationToken)
    {
        var result = await service.GetByDriverIdAsync(
            driverId,
            cancellationToken);

        return Ok(result);
    }
}