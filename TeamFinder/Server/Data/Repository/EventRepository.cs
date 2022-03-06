using TeamFinder.Shared;

namespace TeamFinder.Server.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> GetAll();
    }

    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ApplicationDbContext DbContext { get; }
        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
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

        
    }
}
