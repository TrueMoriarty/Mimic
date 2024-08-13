using System.Linq.Expressions;

namespace DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> : IDisposable
{
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "", bool readOnly = false);

    TEntity GetByID(object id);
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entity);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
}