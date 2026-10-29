using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

public sealed class ClientService : CrudService<Client, ClientDto, ClientCreateDto, ClientUpdateDto>, IClientService
{
    private readonly IRepository<Trip> _trips;
    public ClientService(IRepository<Client> clients, IRepository<Trip> trips) : base(clients) => _trips = trips;
    protected override ClientDto ToDto(Client c) => new(c.Id, c.Name, c.Phone);
    protected override Client FromCreate(ClientCreateDto r, int id) => new() { Id=id, Name=r.Name.Trim(), Phone=r.Phone.Trim() };
    protected override Client FromUpdate(Client c, ClientUpdateDto r)
    {
        c.Name = r.Name.Trim();
        c.Phone = r.Phone.Trim();
        return c;
    }
    public IReadOnlyList<ClientDto> GetByVehicleModel(int vehicleModelId) => _trips.GetAll()
        .Where(t => t.Vehicle.VehicleModel.Id == vehicleModelId).Select(t => t.Client).DistinctBy(c => c.Id)
        .OrderBy(c => c.Name).Select(ToDto).ToList();
    public IReadOnlyList<ClientCostDto> GetTopByTotalCost(int count = 5)
    {
        if (count is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Количество клиентов должно быть от 1 до 100.");
        }

        return _trips.GetAll()
            .GroupBy(trip => trip.Client)
            .Select(group => new ClientCostDto(group.Key.Id, group.Key.Name, group.Sum(trip => trip.Cost)))
            .OrderByDescending(item => item.TotalCost)
            .ThenBy(item => item.ClientName)
            .Take(count)
            .ToList();
    }
}
