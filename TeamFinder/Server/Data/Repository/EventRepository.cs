using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;
using TeamFinder.Shared.Requests;

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
        
        public async Task<List<SportEvent>> GetAllEvents(GetEventsRequest request)
        {
            var events = DbSet.Include(e => e.Users)
                .Include(e => e.Location);
            
            //todo filtering
            
            var ordered = request.OrderBy switch
            {
                GetEventsRequestOrderBy.Name => events.OrderBy(e => e.Name),
                GetEventsRequestOrderBy.Date => events.OrderByDescending(e => e.Date),
                _ => throw new ArgumentOutOfRangeException()
            };

            return await ordered.ToListAsync();
        }
    }
}
