
namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Результат аналитики по суммарной стоимости перевозок клиента
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientName">Имя клиента</param>
/// <param name="TotalCost">Общая стоимость перевозок</param>
public sealed record ClientCostDto(
    int ClientId,
    string ClientName,
    decimal TotalCost);
