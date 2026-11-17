
using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Данные для создания или обновления клиента
/// </summary>
public sealed record ClientUpsertDto(
    [Required, StringLength(200, MinimumLength = 1)] string Name,
    [Required, StringLength(40, MinimumLength = 3)] string Phone);
