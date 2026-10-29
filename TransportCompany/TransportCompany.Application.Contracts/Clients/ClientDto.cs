namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Данные клиента, возвращаемые API.
/// </summary>
/// <param name="Id">идентификатор клиента</param>
/// <param name="Name">имя клиента</param>
/// <param name="Phone">контактный телефон</param>
public sealed record ClientDto(int Id, string Name, string Phone);
