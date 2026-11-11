
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Vehicles;

public interface IVehicleService
    : ICrudService<int, VehicleDto, VehicleUpsertDto>
{
    Task<IReadOnlyList<VehicleDto>> GetVehiclesOnTripsAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<VehicleTripCountDto>> GetTripCountsAsync(
        CancellationToken cancellationToken = default);
}
