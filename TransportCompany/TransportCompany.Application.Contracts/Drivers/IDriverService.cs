
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Drivers;

public interface IDriverService
    : ICrudService<int, DriverDto, DriverUpsertDto>
{
    Task<IReadOnlyList<DriverTripCountDto>> GetTopByTripCountAsync(
        int count = 5,
        CancellationToken cancellationToken = default);
}
