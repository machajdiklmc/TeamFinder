using TeamFinder.Server.Models;
using TeamFinder.Shared.Models;
using SportEvent = TeamFinder.Server.Models.SportEvent;
using UserEvents = TeamFinder.Server.Models.UserEvents;

namespace TeamFinder.Server.Data
{
    using AutoMapper;
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SportEvent, Shared.Models.SportEvent>();
            CreateMap<Shared.Models.SportEvent, SportEvent>();
            CreateMap<Shared.Models.UserEvents, UserEvents>();
            CreateMap<UserEvents, Shared.Models.UserEvents>();
            CreateMap<User, ApplicationUser>();
            CreateMap<ApplicationUser, User>();
        }
    }
}
