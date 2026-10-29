using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Данные для создания клиента.
/// </summary>
/// <param name="Name">имя клиента</param>
/// <param name="Phone">контактный телефон</param>
public sealed record ClientCreateDto([property: Required, StringLength(200, MinimumLength = 1)] string Name, [property: Required, StringLength(40, MinimumLength = 3)] string Phone);
