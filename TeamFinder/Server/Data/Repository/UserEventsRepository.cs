using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data.Repository;

public class UserEventsRepository : RepositoryBase<UserEvents>
{
    public UserEventsRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    protected override DbSet<UserEvents> DbSet => DbContext.UserEvents;

    public async Task<List<Models.SportEvent>> FindUserEvents(string userId, RelationshipType? type)
    {
        return (await DbContext.UserEvents
                .Include(ev => ev.SportEvent)
                .Where(e => e.UserId == userId &&  (type == null || e.Type == type))
                .ToListAsync())
            .Select(events => events.SportEvent)
            .ToList();
    }

    public async Task<bool> JoinEvent(string userId, Guid eventId, RelationshipType type)
    {
        var joinedEvent = await GetSingleOrDefault(e => e.SportEventId == eventId && e.UserId == userId);

        if (joinedEvent is not null)
            return false;

        await Add(new UserEvents
        {
            UserId = userId,
            SportEventId = eventId,
            Type = type
        });

        return true;
    }

    public async Task<bool> LeaveEvent(string userId, Guid eventId)
    {
        var joinedEvent = await DbSet
            .Include(e => e.SportEvent)
            .SingleOrDefaultAsync(e => e.SportEventId == eventId && e.UserId == userId);
        
        if (joinedEvent is null || joinedEvent.Type == RelationshipType.Owner)
            return false;

        await Delete(joinedEvent);
        return true;
    }
}