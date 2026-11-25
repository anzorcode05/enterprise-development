
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
    Task<IReadOnlyList<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    Task<TEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет сущность по идентификатору
    /// </summary>
    Task<bool> UpdateAsync(
        int id,
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}
