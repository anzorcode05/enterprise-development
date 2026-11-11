
namespace TransportCompany.Domain.Entities;

/// <summary>Клиент транспортной компании.</summary>
public sealed class Client
{
    /// <summary>Уникальный идентификатор клиента.</summary>
    public required int Id { get; set; }

    /// <summary>Имя или наименование клиента.</summary>
    public required string Name { get; set; }

    /// <summary>Контактный телефон клиента.</summary>
    public required string Phone { get; set; }
}
