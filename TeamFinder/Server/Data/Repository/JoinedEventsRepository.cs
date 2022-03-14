using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data.Repository;

public class JoinedEventsRepository : RepositoryBase<JoinedEvents>
{
    public JoinedEventsRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    protected override DbSet<JoinedEvents> DbSet => DbContext.JoinedEvents;

    public async Task<List<Models.SportEvent>> FindJoinedEvents(string userId)
    {
        return (await DbContext.JoinedEvents
                .Include(ev => ev.SportEvent)
                .Where(e => e.UserId == userId)
                .ToListAsync())
            .Select(events => events.SportEvent)
            .ToList();
    }

    public async Task<bool> JoinEvent(string userId, Guid eventId)
    {
        var joinedEvent = await GetSingleOrDefault(e => e.SportEventId == eventId && e.UserId == userId);

        if (joinedEvent is not null)
            return false;

        await Add(new JoinedEvents
        {
            UserId = userId,
            SportEventId = eventId
        });

        return true;
    }

    public async Task<bool> LeaveEvent(string userId, Guid eventId)
    {
        var joinedEvent = await GetSingleOrDefault(e => e.SportEventId == eventId && e.UserId == userId);

        if (joinedEvent is null)
            return false;

        await Delete(joinedEvent);
        return true;
    }
}