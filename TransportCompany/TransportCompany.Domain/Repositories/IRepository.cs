
using System.Linq.Expressions;

namespace TransportCompany.Domain.Repositories;

/// <summary>
/// Определяет операции для работы с хранилищем сущностей
/// </summary>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>
    /// Возвращает сущности, соответствующие условию
    /// </summary>
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Возвращает следующий доступный идентификатор
    /// </summary>
    Task<int> GetNextIdAsync();

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Обновляет сущность по идентификатору
    /// </summary>
    Task<bool> UpdateAsync(int id, TEntity entity);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(int id);
}
