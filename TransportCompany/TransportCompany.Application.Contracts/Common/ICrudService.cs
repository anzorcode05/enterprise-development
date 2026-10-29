namespace TransportCompany.Application.Contracts.Common;

public interface ICrudService<TKey, TRead, in TCreate, in TUpdate>
{
    IReadOnlyList<TRead> GetAll();
    TRead? GetById(TKey id);
    TRead Create(TCreate request);
    TRead? Update(TKey id, TUpdate request);
    bool Delete(TKey id);
}
