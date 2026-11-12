
namespace TransportCompany.Application.Contracts.Common;

/// <summary>
/// Определяет базовые асинхронные операции для работы с сущностями
/// </summary>
public interface ICrudService<TKey, TRead, in TUpsert>
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    Task<IReadOnlyList<TRead>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    Task<TRead?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    Task<TRead> CreateAsync(
        TUpsert request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    Task<TRead?> UpdateAsync(
        TKey id,
        TUpsert request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(
        TKey id,
        CancellationToken cancellationToken = default);
}
