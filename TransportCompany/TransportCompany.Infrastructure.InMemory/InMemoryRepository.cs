
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для хранения сущностей в оперативной памяти
/// </summary>
public sealed class InMemoryRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly List<TEntity> _items;
    private readonly Func<TEntity, int> _getId;
    private readonly Action<TEntity, int> _setId;
    private readonly object _lock = new();

    /// <summary>
    /// Создаёт репозиторий и заполняет его начальными данными
    /// </summary>
    public InMemoryRepository(
        IEnumerable<TEntity> seed,
        Func<TEntity, int> getId,
        Action<TEntity, int> setId)
    {
        _items = seed.ToList();
        _getId = getId;
        _setId = setId;
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            IReadOnlyList<TEntity> result = _items.ToList();
            return Task.FromResult(result);
        }
    }

    /// <inheritdoc/>
    public Task<TEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            var entity = _items.FirstOrDefault(item => _getId(item) == id);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc/>
    public Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            var nextId = _items.Count == 0
                ? 1
                : _items.Max(_getId) + 1;

            _setId(entity, nextId);
            _items.Add(entity);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(
        int id,
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            var existing = _items.FirstOrDefault(item => _getId(item) == id);

            return Task.FromResult(
                existing is not null && ReferenceEquals(existing, entity));
        }
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
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
}
