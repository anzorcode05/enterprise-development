using TransportCompany.Application.Contracts.Common;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Application.Services;

public abstract class CrudService<TEntity, TRead, TCreate, TUpdate> : ICrudService<int, TRead, TCreate, TUpdate>
    where TEntity : class
{
    protected readonly IRepository<TEntity> Repository;
    protected CrudService(IRepository<TEntity> repository) => Repository = repository;
    protected abstract TRead ToDto(TEntity entity);
    protected abstract TEntity FromCreate(TCreate request, int id);
    protected abstract TEntity FromUpdate(TEntity current, TUpdate request);

    public IReadOnlyList<TRead> GetAll() => Repository.GetAll().Select(ToDto).ToList();
    public TRead? GetById(int id) => Repository.GetById(id) is { } entity ? ToDto(entity) : default;
    public TRead Create(TCreate request)
    {
        var entity = FromCreate(request, Repository.GetNextId());
        Repository.Add(entity);
        return ToDto(entity);
    }
    public TRead? Update(int id, TUpdate request)
    {
        var current = Repository.GetById(id);
        if (current is null) return default;
        var updated = FromUpdate(current, request);
        return Repository.Update(id, updated) ? ToDto(updated) : default;
    }
    public bool Delete(int id) => Repository.Delete(id);
}
