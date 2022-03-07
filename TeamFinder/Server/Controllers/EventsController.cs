using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamFinder.Server.Data.Repository;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using Event = Duende.IdentityServer.Events.Event;

namespace TeamFinder.Server.Controllers
{
    //[Authorize]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly EventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventsController(ILogger<EventsController> logger, IMapper mapper, EventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet(Endpoints.GetEvents)]
        public async Task<IEnumerable<SportEvent>> Get()
        {
            return _mapper.Map<IEnumerable<SportEvent>>(await _eventRepository.GetAll());
        }

        [HttpPost(Endpoints.AddEvent)]
        public async Task AddEvent([FromBody] SportEvent ev)
        {
            await _eventRepository.Add(_mapper.Map<Models.SportEvent>(ev));
        }
    }
}