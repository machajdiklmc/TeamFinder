using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data
{
    using AutoMapper;
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SportEvent, Shared.Models.SportEvent>();
            CreateMap<Shared.Models.SportEvent, SportEvent>();
        }
    }
}
