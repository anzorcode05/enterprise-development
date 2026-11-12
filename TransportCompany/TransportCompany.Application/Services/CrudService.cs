
using TransportCompany.Application.Contracts.Common;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

public abstract class CrudService<TEntity, TRead, TUpsert>
    : ICrudService<int, TRead, TUpsert>
    where TEntity : class
{
    protected readonly IRepository<TEntity> Repository;

    protected CrudService(IRepository<TEntity> repository)
    {
        Repository = repository;
    }

    protected abstract TRead ToDto(TEntity entity);

    protected abstract Task<TEntity> CreateEntityAsync(
        TUpsert request,
        int id);

    protected abstract Task UpdateEntityAsync(
        TEntity entity,
        TUpsert request);

    public async Task<IReadOnlyList<TRead>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entities = await Repository.GetAllAsync();

        return entities.Select(ToDto).ToList();
    }

    public async Task<TRead?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await Repository.GetByIdAsync(id);

        return entity is null ? default : ToDto(entity);
    }

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

    public virtual async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Repository.DeleteAsync(id);
    }
}
