
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Базовая реализация репозитория для PostgreSQL
/// </summary>
public abstract class PostgreSqlRepository<TEntity>(
    TransportCompanyDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected TransportCompanyDbContext Context { get; } = context;

    /// <summary>
    /// Набор сущностей текущего типа
    /// </summary>
    protected DbSet<TEntity> Entities => Context.Set<TEntity>();

    /// <inheritdoc/>
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await Entities.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> UpdateAsync(
        int id,
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        var existingEntity = await Entities.FindAsync([id], cancellationToken);

        if (existingEntity is null)
        {
            return false;
        }

        var existingEntry = Context.Entry(existingEntity);
        var incomingValues = Context.Entry(entity).CurrentValues;

        foreach (var property in existingEntry.Properties)
        {
            if (property.Metadata.IsPrimaryKey())
            {
                continue;
            }

            property.CurrentValue = incomingValues[property.Metadata];
        }

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <inheritdoc/>
    public virtual async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FindAsync([id], cancellationToken);

        if (entity is null)
        {
            return false;
        }

        Entities.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
