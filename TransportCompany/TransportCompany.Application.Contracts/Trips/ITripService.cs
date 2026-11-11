
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Trips;

public interface ITripService
    : ICrudService<int, TripDto, TripUpsertDto>
{
    Task<IReadOnlyList<TripDto>> GetByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TripDto>> GetByDriverIdAsync(
        int driverId,
        CancellationToken cancellationToken = default);
}
