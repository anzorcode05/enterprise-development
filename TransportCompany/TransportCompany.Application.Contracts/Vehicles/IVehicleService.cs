using TransportCompany.Application.Contracts.Common;
namespace TransportCompany.Application.Contracts.Vehicles;
public interface IVehicleService : ICrudService<int, VehicleDto, VehicleCreateDto, VehicleUpdateDto>
{
    IReadOnlyList<VehicleDto> GetVehiclesOnTrips();
    IReadOnlyList<VehicleTripCountDto> GetTripCounts();
}
