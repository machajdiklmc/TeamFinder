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

        public override async Task Add(SportEvent entity)
        {
            DbSet.Add(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<SportEvent?> GetEvent(Guid sportEventId)
        {
            return await DbSet.Include(e => e.Users)
                .Include(e => e.Location)
                .Where(e => e.Id == sportEventId)
                .SingleOrDefaultAsync();
        }
        
        public async Task<List<SportEvent>> GetAllEvents()
        {
            return await DbSet.Include(e => e.Users)
                .Include(e => e.Location)
                .ToListAsync();
        }
    }
}
