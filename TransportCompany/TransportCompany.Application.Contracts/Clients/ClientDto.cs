namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Данные клиента, возвращаемые API
/// </summary>
public sealed record ClientDto(
    int Id,
    string Name,
    string Phone);