
using System.Linq.Expressions;

namespace TransportCompany.Domain.Repositories;

/// <summary>Определяет асинхронные операции хранения доменных сущностей.</summary>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>Асинхронно возвращает все сущности.</summary>
    public Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>Асинхронно возвращает сущности, удовлетворяющие условию.</summary>
    public Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>Асинхронно возвращает сущность по идентификатору.</summary>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>Асинхронно возвращает следующий доступный идентификатор.</summary>
    public Task<int> GetNextIdAsync();

    /// <summary>Асинхронно добавляет сущность.</summary>
    public Task AddAsync(TEntity entity);

    /// <summary>Асинхронно заменяет сущность с указанным идентификатором.</summary>
    public Task<bool> UpdateAsync(int id, TEntity entity);

    /// <summary>Асинхронно удаляет сущность по идентификатору.</summary>
    public Task<bool> DeleteAsync(int id);
}
