
using TransportCompany.Application.Contracts.Common;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

/// <summary>
/// Базовый сервис для выполнения CRUD-операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TRead">Тип DTO для чтения</typeparam>
/// <typeparam name="TUpsert">Тип DTO для создания и обновления</typeparam>
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
    /// <param name="repository">Репозиторий сущностей</param>
    protected CrudService(IRepository<TEntity> repository)
    {
        Repository = repository;
    }

    /// <summary>
    /// Преобразует сущность в DTO
    /// </summary>
    /// <param name="entity">Сущность для преобразования</param>
    /// <returns>Объект DTO</returns>
    protected abstract TRead ToDto(TEntity entity);

    /// <summary>
    /// Создаёт сущность на основе входных данных
    /// </summary>
    /// <param name="request">Данные для создания</param>
    /// <param name="id">Идентификатор новой сущности</param>
    /// <returns>Созданная сущность</returns>
    protected abstract Task<TEntity> CreateEntityAsync(
        TUpsert request,
        int id);

    /// <summary>
    /// Обновляет данные существующей сущности
    /// </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <param name="request">Новые данные</param>
    protected abstract Task UpdateEntityAsync(
        TEntity entity,
        TUpsert request);

    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список DTO</returns>
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
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO сущности или null, если запись не найдена</returns>
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
    /// <param name="request">Данные для создания</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO созданной сущности</returns>
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
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="request">Новые данные</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO обновлённой сущности или null, если запись не найдена</returns>
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
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>true, если сущность удалена</returns>
    public virtual async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Repository.DeleteAsync(id);
    }
}
