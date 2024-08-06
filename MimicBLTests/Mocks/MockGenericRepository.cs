using DAL.Repositories;
using System.Linq.Expressions;

namespace MimicBLTests.Mocks;

internal class MockGenericRepository<TEntity> : IGenericRepository<TEntity>
{
    readonly ICollection<TEntity> storage;

    public MockGenericRepository(ICollection<TEntity> entitiesStorage)
    {
        storage = entitiesStorage;
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = storage.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            // ничего нет
        }

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public TEntity GetByID(object id) => throw new NotImplementedException(); //storage.FirstOrDefault(e => e.) А как ?)

    public void Insert(TEntity entity) => storage.Add(entity);

    public void Delete(object id)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entityToDelete) => storage.Remove(entityToDelete);

    public void Update(TEntity entityToUpdate)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}