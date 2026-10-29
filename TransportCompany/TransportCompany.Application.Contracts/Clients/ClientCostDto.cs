namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Результат аналитики по суммарной стоимости перевозок клиента.
/// </summary>
/// <param name="ClientId">идентификатор клиента</param>
/// <param name="ClientName">имя клиента</param>
/// <param name="TotalCost">общая стоимость его перевозок</param>
public sealed record ClientCostDto(int ClientId, string ClientName, decimal TotalCost);
