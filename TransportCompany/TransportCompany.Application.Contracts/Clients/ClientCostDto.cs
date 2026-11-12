namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Результат аналитики по суммарной стоимости перевозок клиента
/// </summary>
public sealed record ClientCostDto(
    int ClientId,
    string ClientName,
    decimal TotalCost);