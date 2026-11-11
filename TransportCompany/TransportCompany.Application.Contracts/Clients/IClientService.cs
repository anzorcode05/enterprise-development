
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Application.Contracts.Clients;

public interface IClientService
    : ICrudService<int, ClientDto, ClientUpsertDto>
{
    Task<IReadOnlyList<ClientDto>> GetByVehicleModelAsync(
        int vehicleModelId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ClientCostDto>> GetTopByTotalCostAsync(
        int count = 5,
        CancellationToken cancellationToken = default);
}
