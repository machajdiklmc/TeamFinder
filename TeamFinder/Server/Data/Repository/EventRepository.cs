using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAll();
        public Task Add(TEntity entity);
    }

    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected abstract DbSet<TEntity> DbSet { get; }
        public ApplicationDbContext DbContext { get; }
        public async Task<IEnumerable<TEntity>> GetAll() => await DbSet.ToListAsync();

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
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
