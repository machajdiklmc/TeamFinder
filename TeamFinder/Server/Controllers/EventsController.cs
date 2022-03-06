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

        public EventsController(ILogger<EventsController> logger, EventRepository eventRepository)
        {
            _logger = logger;
        }

        [HttpGet(Endpoints.GetEvents)]
        public IEnumerable<SportEvent> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new SportEvent()
                {
                    Name = "QWE",
                    Date = DateTime.Now
                })
                .ToList();
        }
    }
}