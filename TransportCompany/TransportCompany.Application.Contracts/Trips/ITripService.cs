using TransportCompany.Application.Contracts.Common;
namespace TransportCompany.Application.Contracts.Trips;
public interface ITripService : ICrudService<int, TripDto, TripCreateDto, TripUpdateDto> { }
