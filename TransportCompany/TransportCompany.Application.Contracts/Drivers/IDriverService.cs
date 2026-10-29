using TransportCompany.Application.Contracts.Common;
namespace TransportCompany.Application.Contracts.Drivers;
public interface IDriverService : ICrudService<int, DriverDto, DriverCreateDto, DriverUpdateDto>
{
    IReadOnlyList<DriverTripCountDto> GetTopByTripCount(int count = 5);
}
