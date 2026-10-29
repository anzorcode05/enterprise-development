using TransportCompany.Domain.Repositories;
namespace TransportCompany.Infrastructure.InMemory;

/// <summary>Простое хранилище в памяти для лабораторной работы</summary>
public sealed class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly List<TEntity> _items;
    private readonly Func<TEntity,int> _getId;
    public InMemoryRepository(IEnumerable<TEntity> seed, Func<TEntity,int> getId) { _items=seed.ToList(); _getId=getId; }
    public IReadOnlyList<TEntity> GetAll() => _items.ToList();
    public TEntity? GetById(int id) => _items.FirstOrDefault(x=>_getId(x)==id);
    public int GetNextId() => _items.Count == 0 ? 1 : _items.Max(_getId)+1;
    public void Add(TEntity entity) { if (_items.Any(x=>_getId(x)==_getId(entity))) throw new InvalidOperationException("Запись с таким идентификатором уже существует."); _items.Add(entity); }
    public bool Update(int id,TEntity entity) { var index=_items.FindIndex(x=>_getId(x)==id); if(index<0)return false; _items[index]=entity; return true; }
    public bool Delete(int id) { var index=_items.FindIndex(x=>_getId(x)==id); if(index<0)return false; _items.RemoveAt(index); return true; }
}
