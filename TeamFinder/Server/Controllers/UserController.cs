using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamFinder.Server.Data.Repository;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using RelationshipType = TeamFinder.Server.Models.RelationshipType;

namespace TeamFinder.Server.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly UserEventsRepository _userEventsRepository;
        private readonly UserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IMapper mapper,
            UserEventsRepository userEventsRepository,
            UserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userEventsRepository = userEventsRepository;
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpPost(Endpoints.GetUser)]
        public async Task<User> FindUser([FromBody] string id)
        {
            var a = _mapper.Map<User>(await _userRepository.GetUser(id));
            return a;
        }
    }
}
