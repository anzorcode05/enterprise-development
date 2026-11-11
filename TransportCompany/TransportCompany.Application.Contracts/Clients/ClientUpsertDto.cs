
using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Данные для создания или обновления клиента
/// </summary>
/// <param name="Name">Имя клиента</param>
/// <param name="Phone">Контактный телефон</param>
public sealed record ClientUpsertDto(
    [property: Required, StringLength(200, MinimumLength = 1)] string Name,
    [property: Required, StringLength(40, MinimumLength = 3)] string Phone);
