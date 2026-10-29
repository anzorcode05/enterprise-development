namespace TransportCompany.Domain.Repositories;

/// <summary>Базовый контракт хранения доменных сущностей</summary>
public interface IRepository<TEntity> where TEntity : class
{
    IReadOnlyList<TEntity> GetAll();
    TEntity? GetById(int id);
    int GetNextId();
    void Add(TEntity entity);
    bool Update(int id, TEntity entity);
    bool Delete(int id);
}
