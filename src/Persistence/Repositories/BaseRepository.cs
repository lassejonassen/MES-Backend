using Abstractions;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbContext DbContext => context;

    public TEntity Add(TEntity entity)
    {
        return DbContext.Set<TEntity>().Add(entity).Entity;
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}