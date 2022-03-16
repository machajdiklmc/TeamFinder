using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamFinder.Server.Data.Repository;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using RelationshipType = TeamFinder.Server.Models.RelationshipType;
using SportEvent = TeamFinder.Shared.Models.SportEvent;

namespace TeamFinder.Server.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly EventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly UserEventsRepository _userEventsRepository;
        private readonly UserRepository _userRepository;

        public EventsController(ILogger<EventsController> logger, IMapper mapper,
            UserEventsRepository userEventsRepository,
            UserRepository userRepository,
            EventRepository eventRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _eventRepository = eventRepository;
            _userEventsRepository = userEventsRepository;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet(Endpoints.GetAllEvents)]
        public async Task<List<SportEvent>> Get()
        {
            return _mapper.Map<List<SportEvent>>(await _eventRepository.GetAll());
        }

        [HttpPost(Endpoints.GetAllUsersInEvent)]
        public async Task<List<SportEvent>> GetAllUsersInEvent([FromBody] Guid eventId)
        {
            var a = _mapper.Map<List<SportEvent>>(await _userEventsRepository.FindUserEventsByEvent(eventId));
            return a;
        }
        [AllowAnonymous]
        [HttpPost(Endpoints.GetUserEvents)]
        public async Task<List<Models.UserEvents>> FindUserEvents([FromBody] UserEventsRequest request)
        {
            var a = _mapper.Map<List<Models.UserEvents>>(await _userEventsRepository.FindUserEventsByUser(request.UserId, (RelationshipType?)request.Type));
            return a;
        }
        
        [AllowAnonymous]
        [HttpPost(Endpoints.AddEvent)]
        public async Task AddEvent([FromBody] SportEvent ev)
        {
            await _eventRepository.Add(_mapper.Map<Models.SportEvent>(ev));
            await _userEventsRepository.JoinEvent(ev.OwnerId, ev.Id, RelationshipType.Owner);
        }

        [AllowAnonymous]
        [HttpPost(Endpoints.JoinEvent)]
        public async Task<bool> JoinEvent([FromBody] UserEvents userEvent)
        {
            return await _userEventsRepository.JoinEvent(userEvent.UserId, userEvent.SportEventId, RelationshipType.Joined);
        }

        [AllowAnonymous]
        [HttpPost(Endpoints.LeaveEvent)]
        public async Task<bool> LeaveEvent([FromBody] UserEvents userEvent)
        {
            return await _userEventsRepository.LeaveEvent(userEvent.UserId, userEvent.SportEventId);
        }
    }
}