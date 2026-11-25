namespace TransportCompany.Domain.ReadModels;

/// <summary>
/// Сводка по общей стоимости рейсов клиента
/// </summary>
public sealed record ClientCostSummary(
    int ClientId,
    string ClientName,
    decimal TotalCost);