using DAL.EfCode;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories;

/// <summary>
/// Этот универсальный репозиторий будет обрабатывать типичные требования CRUD. 
/// Если к определенному типу сущности предъявляют особые требования, такие как более 
/// сложная фильтрация или упорядочивание, можно создать производный класс с дополнительными 
/// методами для этого типа.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
{
    internal MimicContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(MimicContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties
                     .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public virtual TEntity GetByID(object id)
    {
        return dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public void InsertRange(IEnumerable<TEntity> entity)
    {
        dbSet.AddRange(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }

        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}