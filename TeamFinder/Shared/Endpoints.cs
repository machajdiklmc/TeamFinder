using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinder.Shared
{
    public static class Endpoints
    {
        public const string GetAllEvents = "api/events";
        public const string GetEvent = "api/event";
        public const string AddEvent = "api/addevent";
        public const string JoinEvent = "api/joinevent";
        public const string LeaveEvent = "api/leaveevent";
        public const string GetUserEvents = "api/getusereventsbyuser";
        public const string GetUser = "api/getuser/{userId}";
        public const string GetAllUsersInEvent = "api/getusereventsbyevent";
    }
}
