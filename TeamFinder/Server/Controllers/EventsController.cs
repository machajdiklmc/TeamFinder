using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamFinder.Server.Data.Repository;
using TeamFinder.Shared;
using Event = Duende.IdentityServer.Events.Event;

namespace TeamFinder.Server.Controllers
{
    //[Authorize]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly EventRepository _eventRepository;

        public EventsController(ILogger<EventsController> logger, EventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        [HttpGet(Endpoints.GetEvents)]
        public async Task<IEnumerable<SportEvent>> Get()
        {
            return (await _eventRepository.GetAll()).Select(e => new SportEvent() { Date = e.Date, Name = e.Name});
        }

        [HttpPost(Endpoints.AddEvent)]
        public async Task AddEvent([FromBody] SportEvent ev)
        {
            await _eventRepository.Add(new Models.SportEvent
            {
                Id = Guid.NewGuid(),
                Name = ev.Name,
                Date = ev.Date
            });
        }
    }
}