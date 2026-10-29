using TransportCompany.Application.Contracts.Common;
namespace TransportCompany.Application.Contracts.Clients;
public interface IClientService : ICrudService<int, ClientDto, ClientCreateDto, ClientUpdateDto>
{
    IReadOnlyList<ClientDto> GetByVehicleModel(int vehicleModelId);
    IReadOnlyList<ClientCostDto> GetTopByTotalCost(int count = 5);
}
