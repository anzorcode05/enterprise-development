
namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Данные клиента, возвращаемые API
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="Name">Имя клиента</param>
/// <param name="Phone">Контактный телефон</param>
public sealed record ClientDto(
    int Id,
    string Name,
    string Phone);
