using System.Linq.Expressions;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для хранения сущностей в оперативной памяти
/// </summary>
/// <typeparam name="TEntity">Тип хранимой сущности</typeparam>
public sealed class InMemoryRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly List<TEntity> _items;
    private readonly Func<TEntity, int> _getId;

    /// <summary>
    /// Создаёт репозиторий и заполняет его начальными данными
    /// </summary>
    /// <param name="seed">Начальная коллекция сущностей</param>
    /// <param name="getId">Функция получения идентификатора сущности</param>
    public InMemoryRepository(
        IEnumerable<TEntity> seed,
        Func<TEntity, int> getId)
    {
        _items = seed.ToList();
        _getId = getId;
    }

    /// <summary>
    /// Возвращает копию списка всех сущностей
    /// </summary>
    /// <returns>Список сохранённых сущностей</returns>
    public Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        IReadOnlyList<TEntity> result = _items.ToList();
        return Task.FromResult(result);
    }

    /// <summary>
    /// Возвращает сущности, соответствующие заданному условию
    /// </summary>
    /// <param name="predicate">Условие отбора сущностей</param>
    /// <returns>Список найденных сущностей</returns>
    public Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate)
    {
        var filter = predicate.Compile();
        IReadOnlyList<TEntity> result = _items.Where(filter).ToList();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность или null</returns>
    public Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = _items.FirstOrDefault(item => _getId(item) == id);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Возвращает следующий доступный идентификатор
    /// </summary>
    /// <returns>Следующий идентификатор</returns>
    public Task<int> GetNextIdAsync()
    {
        var nextId = _items.Count == 0
            ? 1
            : _items.Max(_getId) + 1;

        return Task.FromResult(nextId);
    }

    /// <summary>
    /// Добавляет новую сущность в репозиторий
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если сущность с таким идентификатором уже существует
    /// </exception>
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

    /// <summary>
    /// Обновляет сущность с указанным идентификатором
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="entity">Новые данные сущности</param>
    /// <returns>true, если сущность найдена и обновлена</returns>
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

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    /// <returns>true, если сущность найдена и удалена</returns>
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