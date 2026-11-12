
using System.Linq.Expressions;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.InMemory;

/// <summary>Хранилище сущностей в памяти</summary>
public sealed class InMemoryRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly List<TEntity> _items;
    private readonly Func<TEntity, int> _getId;

    public InMemoryRepository(
        IEnumerable<TEntity> seed,
        Func<TEntity, int> getId)
    {
        _items = seed.ToList();
        _getId = getId;
    }

    public Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        IReadOnlyList<TEntity> result = _items.ToList();
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate)
    {
        var filter = predicate.Compile();
        IReadOnlyList<TEntity> result = _items.Where(filter).ToList();

        return Task.FromResult(result);
    }

    public Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = _items.FirstOrDefault(item => _getId(item) == id);
        return Task.FromResult(entity);
    }

    public Task<int> GetNextIdAsync()
    {
        var nextId = _items.Count == 0
            ? 1
            : _items.Max(_getId) + 1;

        return Task.FromResult(nextId);
    }

    public Task AddAsync(TEntity entity)
    {
        if (_items.Any(item => _getId(item) == _getId(entity)))
        {
            throw new InvalidOperationException(
                "Запись с таким идентификатором уже существует");
        }

        _items.Add(entity);
        return Task.CompletedTask;
    }

    public Task<bool> UpdateAsync(int id, TEntity entity)
    {
        var index = _items.FindIndex(item => _getId(item) == id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        _items[index] = entity;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var index = _items.FindIndex(item => _getId(item) == id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        _items.RemoveAt(index);
        return Task.FromResult(true);
    }
}
