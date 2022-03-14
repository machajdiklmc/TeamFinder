using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamFinder.Server.Data.Repository;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
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
            return await _joinedEventsRepository.JoinEvent(joinedEvent.UserId, joinedEvent.SportEventId);
        }
        
        [HttpPost(Endpoints.LeaveEvent)]
        public async Task<bool> LeaveEvent([FromBody] JoinedEvents joinedEvent)
        {
            return await _joinedEventsRepository.LeaveEvent(joinedEvent.UserId, joinedEvent.SportEventId);
        }
    }
}