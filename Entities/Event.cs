using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventbriteNET.HttpApi;

namespace EventbriteNET.Entities
{
    public class Event : EntityBase
    {
        public long Id;
        public string Title;
        public string Description;
        public string Status;
        public DateTime StartDateTime;
        public DateTime EndDateTime;
        public string Timezone;
        public DateTime Created;
        public DateTime Modified;
        public Venue Venue { get; set; }

        public Dictionary<long, Ticket> Tickets = new Dictionary<long, Ticket>();

        private List<Attendee> attendees;
        public List<Attendee> Attendees
        {
            get
            {
                if (this.attendees == null)
                {
                    this.attendees = new List<Attendee>();
                    this.attendees.AddRange(new EventAttendeesRequest(this.Id, Context).GetResponse());
                }
                return attendees;
            }
        }

        public Event(EventbriteContext context) : base(context) { }

        public string Url { get; set; }
    }
}
