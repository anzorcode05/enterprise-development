
using TransportCompany.Application.Contracts.Common;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Базовый сервис для выполнения CRUD-операций
/// </summary>
public abstract class CrudService<TEntity, TRead, TUpsert>
    : ICrudService<int, TRead, TUpsert>
    where TEntity : class
{
    /// <summary>
    /// Репозиторий для работы с сущностями
    /// </summary>
    protected readonly IRepository<TEntity> Repository;

    /// <summary>
    /// Создаёт базовый CRUD-сервис
    /// </summary>
    protected CrudService(IRepository<TEntity> repository)
    {
        Repository = repository;
    }

    /// <summary>
    /// Преобразует сущность в DTO
    /// </summary>
    protected abstract TRead ToDto(TEntity entity);

    /// <summary>
    /// Создаёт сущность на основе входных данных
    /// </summary>
    protected abstract Task<TEntity> CreateEntityAsync(
        TUpsert request,
        int id);

    /// <summary>
    /// Обновляет данные существующей сущности
    /// </summary>
    protected abstract Task UpdateEntityAsync(
        TEntity entity,
        TUpsert request);

    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public async Task<IReadOnlyList<TRead>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entities = await Repository.GetAllAsync();

        return entities.Select(ToDto).ToList();
    }

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public async Task<TRead?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await Repository.GetByIdAsync(id);

        return entity is null ? default : ToDto(entity);
    }

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    public async Task<TRead> CreateAsync(
        TUpsert request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var id = await Repository.GetNextIdAsync();
        var entity = await CreateEntityAsync(request, id);

        await Repository.AddAsync(entity);

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
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await Repository.GetByIdAsync(id);

        if (entity is null)
        {
            return default;
        }

        await UpdateEntityAsync(entity, request);

        var updated = await Repository.UpdateAsync(id, entity);

        return updated ? ToDto(entity) : default;
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    public virtual async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Repository.DeleteAsync(id);
    }
}
