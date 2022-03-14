using System.Linq.Expressions;

namespace TeamFinder.Server.Data.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<List<TEntity>> GetAll();
    public Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    public Task Add(TEntity entity);
    public Task Update(TEntity entity);
    public Task Delete(TEntity entity);
}