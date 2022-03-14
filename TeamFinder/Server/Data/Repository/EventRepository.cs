using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data.Repository
{
    public class EventRepository : RepositoryBase<SportEvent>
    {
        public EventRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        protected override DbSet<SportEvent> DbSet => DbContext.Events;
    }
}
