using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetAll();
        public Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        public Task Add(TEntity entity);
        public Task Update(TEntity entity);
        public Task Delete(TEntity entity);
    }

    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected abstract DbSet<TEntity> DbSet { get; }
        public ApplicationDbContext DbContext { get; }
        public async Task<List<TEntity>> GetAll() => await DbSet.ToListAsync();

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task Update(TEntity entity)
        {
            DbContext.Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        protected RepositoryBase(ApplicationDbContext dataContext)
        {
            DbContext = dataContext;
        }
    }
    public class EventRepository : RepositoryBase<SportEvent>
    {
        public EventRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }


        protected override DbSet<SportEvent> DbSet => DbContext.Events;
    }
}
