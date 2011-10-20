using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventbriteNET.Entities;
using EventbriteNET.Xml;

namespace EventbriteNET.HttpApi
{
    public class UserEventsRequest: RequestBase
    {
        const string PATH = "user_list_events";

        public UserEventsRequest( EventbriteContext context, string[] statuses =null)
            : base(PATH, context)
        {
            //statuses = statuses ?? new string[] {"live", "started"};
            //this.AddGet("event_statuses", String.Join(",", statuses));
        }

        public Event[] GetResponse()
        {
            return new UserEventsBuilder(this.Context).Build(base.GetResponse());
        }
    
    }
}
