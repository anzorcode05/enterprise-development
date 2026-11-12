
namespace TransportCompany.Application.Contracts.Common;

/// <summary>
/// Определяет базовые асинхронные операции для работы с сущностями
/// </summary>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
/// <typeparam name="TRead">Тип данных для чтения</typeparam>
/// <typeparam name="TUpsert">Тип данных для создания и обновления</typeparam>
public interface ICrudService<TKey, TRead, in TUpsert>
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список сущностей</returns>
    Task<IReadOnlyList<TRead>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Найденная сущность или null</returns>
    Task<TRead?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    /// <param name="request">Данные для создания</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Созданная сущность</returns>
    Task<TRead> CreateAsync(
        TUpsert request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="request">Данные для обновления</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Обновлённая сущность или null</returns>
    Task<TRead?> UpdateAsync(
        TKey id,
        TUpsert request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>true, если сущность удалена</returns>
    Task<bool> DeleteAsync(
        TKey id,
        CancellationToken cancellationToken = default);
}
