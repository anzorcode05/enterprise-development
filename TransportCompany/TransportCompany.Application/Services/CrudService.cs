
using TransportCompany.Application.Contracts.Common;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Базовый сервис для выполнения CRUD-операций
/// </summary>
public abstract class CrudService<TEntity, TRead, TUpsert>(
    IRepository<TEntity> repository)
    : ICrudService<int, TRead, TUpsert>
    where TEntity : class
{
    /// <summary>
    /// Репозиторий для работы с сущностями
    /// </summary>
    protected readonly IRepository<TEntity> Repository = repository;

    /// <summary>
    /// Преобразует сущность в DTO
    /// </summary>
    protected abstract TRead ToDto(TEntity entity);

    /// <summary>
    /// Создаёт сущность на основе входных данных
    /// </summary>
    protected abstract Task<TEntity> CreateEntityAsync(
        TUpsert request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет данные существующей сущности
    /// </summary>
    protected abstract Task UpdateEntityAsync(
        TEntity entity,
        TUpsert request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public async Task<IReadOnlyList<TRead>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var entities = await Repository.GetAllAsync(cancellationToken);

        return entities.Select(ToDto).ToList();
    }

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public async Task<TRead?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);

        return entity is null ? default : ToDto(entity);
    }

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    public async Task<TRead> CreateAsync(
        TUpsert request,
        CancellationToken cancellationToken = default)
    {
        var entity = await CreateEntityAsync(request, cancellationToken);

        await Repository.AddAsync(entity, cancellationToken);

        return ToDto(entity);
    }

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    public async Task<TRead?> UpdateAsync(
        int id,
        TUpsert request,
        CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return default;
        }

        await UpdateEntityAsync(entity, request, cancellationToken);

        var updated = await Repository.UpdateAsync(
            id,
            entity,
            cancellationToken);

        return updated ? ToDto(entity) : default;
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    public virtual Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return Repository.DeleteAsync(id, cancellationToken);
    }
}
