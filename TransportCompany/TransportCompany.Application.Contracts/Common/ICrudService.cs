
namespace TransportCompany.Application.Contracts.Common;

public interface ICrudService<TKey, TRead, in TUpsert>
{
    Task<IReadOnlyList<TRead>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TRead?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    Task<TRead> CreateAsync(
        TUpsert request,
        CancellationToken cancellationToken = default);

    Task<TRead?> UpdateAsync(
        TKey id,
        TUpsert request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        TKey id,
        CancellationToken cancellationToken = default);
}
