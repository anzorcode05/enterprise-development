using TransportCompany.Application.Contracts.Common;
namespace TransportCompany.Application.Contracts.VehicleModels;
public interface IVehicleModelService : ICrudService<int, VehicleModelDto, VehicleModelCreateDto, VehicleModelUpdateDto> { }
