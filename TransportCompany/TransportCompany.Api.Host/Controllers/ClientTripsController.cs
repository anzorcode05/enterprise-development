
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Application.Contracts.Trips;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Обрабатывает запросы для получения рейсов клиентов
/// </summary>
[ApiController]
[Route("api/clients/{clientId:int}/trips")]
public sealed class ClientTripsController(
    IClientService clientService,
    ITripService tripService) : ControllerBase
{
    /// <summary>
    /// Возвращает список рейсов указанного клиента
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<TripDto>>> GetByClientId(
        int clientId,
        CancellationToken cancellationToken)
    {
        var client = await clientService.GetByIdAsync(clientId, cancellationToken);

        if (client is null)
        {
            return NotFound();
        }

        var trips = await tripService.GetByClientIdAsync(clientId, cancellationToken);

        return Ok(trips);
    }
}
