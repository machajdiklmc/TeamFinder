using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Data;
using TeamFinder.Server.Data.Repository;
using TeamFinder.Server.Models;
using TeamFinder.Shared;
using Event = Duende.IdentityServer.Events.Event;
using SportEvent = TeamFinder.Shared.Models.SportEvent;

namespace TeamFinder.Server.Controllers
{
    //[Authorize]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly EventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly JoinedEventsRepository _joinedEventsRepository;
        private readonly UserRepository _userRepository;

        public EventsController(ILogger<EventsController> logger, IMapper mapper,
            JoinedEventsRepository joinedEventsRepository,
            UserRepository userRepository,
            EventRepository eventRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _eventRepository = eventRepository;
            _joinedEventsRepository = joinedEventsRepository;
            _userRepository = userRepository;
        }

        [HttpGet(Endpoints.GetAllEvents)]
        public async Task<List<SportEvent>> Get()
        {
            return _mapper.Map<List<SportEvent>>(await _eventRepository.GetAll());
        }
        
        [HttpGet(Endpoints.GetUserJoinedEvents)]
        public async Task<List<SportEvent>> FindUserJoinedEvents([FromQuery] string userId)
        {
            var a = _mapper.Map<List<SportEvent>>(await _joinedEventsRepository.FindJoinedEvents(userId));
            return a;
        }

        [HttpPost(Endpoints.AddEvent)]
        public async Task AddEvent([FromBody] SportEvent ev)
        {
            await _eventRepository.Add(_mapper.Map<Models.SportEvent>(ev));
        }

        [HttpPost(Endpoints.JoinEvent)]
        public async Task<bool> JoinEvent([FromBody] JoinedEvents joinedEvent)
        {
            var a = await _joinedEventsRepository.FindJoinedEvents(joinedEvent.UserId);
            return await _joinedEventsRepository.JoinEvent(joinedEvent.UserId, joinedEvent.SportEventId);
        }

        [HttpPost(Endpoints.LeaveEvent)]
        public async Task<bool> LeaveEvent([FromBody] JoinedEvents joinedEvent)
        {
            var a = await _joinedEventsRepository.FindJoinedEvents(joinedEvent.UserId);
            return await _joinedEventsRepository.LeaveEvent(joinedEvent.UserId, joinedEvent.SportEventId);
        }
    }

    public class UserRepository : RepositoryBase<ApplicationUser>
    {
        public UserRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        protected override DbSet<ApplicationUser> DbSet => DbContext.Users;
    }

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
}