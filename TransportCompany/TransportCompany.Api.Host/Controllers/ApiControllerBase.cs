
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.Common;

namespace TransportCompany.Api.Host.Controllers;

/// <summary>
/// Предоставляет общие CRUD-операции для API-контроллеров
/// </summary>
[ApiController]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public abstract class ApiControllerBase<TRead, TUpsert>(
    ICrudService<int, TRead, TUpsert> service) : ControllerBase
{
    /// <summary>
    /// Возвращает идентификатор элемента
    /// </summary>
    protected abstract int GetId(TRead item);

    /// <summary>
    /// Возвращает список всех элементов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TRead>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает элемент по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TRead>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создаёт новый элемент
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<TRead>> Create(
        [FromBody] TUpsert request,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = GetId(result) },
            result);
    }

    /// <summary>
    /// Обновляет элемент по идентификатору
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TRead>> Update(
        int id,
        [FromBody] TUpsert request,
        CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Удаляет элемент по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}