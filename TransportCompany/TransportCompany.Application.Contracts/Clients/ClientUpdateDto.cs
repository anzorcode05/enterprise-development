using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Application.Contracts.Clients;

/// <summary>
/// Изменяемые данные клиента.
/// </summary>
/// <param name="Name">новое имя клиента</param>
/// <param name="Phone">новый контактный телефон</param>
public sealed record ClientUpdateDto([property: Required, StringLength(200, MinimumLength = 1)] string Name, [property: Required, StringLength(40, MinimumLength = 3)] string Phone);
