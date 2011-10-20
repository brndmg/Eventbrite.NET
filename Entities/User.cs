using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventbriteNET.HttpApi;

namespace EventbriteNET.Entities
{
    public class User
    {
        EventbriteContext _context;

        public User(EventbriteContext context)
        {
            this._context = context;
        }

        private IDictionary<long, Event> userevents;
        public IDictionary<long, Event> Events
        {
            get
            {
                if (userevents == null)
                {
                    userevents = new Dictionary<long, Event>();
                    var eventArray = new UserEventsRequest(_context).GetResponse();
                    foreach (var eventEntity in eventArray)
                    {
                        userevents.Add(eventEntity.Id, eventEntity);
                    }
                }
                return userevents;
            }

        }
    }
}
