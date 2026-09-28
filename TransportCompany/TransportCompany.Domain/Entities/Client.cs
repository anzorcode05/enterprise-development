namespace TransportCompany.Domain.Entities;

/// <summary>
/// Клиент транспортной компании
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента или название организации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public required string Phone { get; set; }
}