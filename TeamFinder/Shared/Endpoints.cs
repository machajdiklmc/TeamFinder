﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinder.Shared
{
    public class Endpoints
    {
        public const string GetAllEvents = "api/events";
        public const string AddEvent = "api/addevent";
        public const string JoinEvent = "api/joinevent";
        public const string LeaveEvent = "api/leaveevent";
        public const string GetUserJoinedEvents = "api/userjoinedevents/{userId}";
    }
}
