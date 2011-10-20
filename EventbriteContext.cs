using EventbriteNET.Entities;
using EventbriteNET.HttpApi;

namespace EventbriteNET
{
    public class EventbriteContext
    {
        public string AppKey;
        public string UserKey;
        public string Host = "https://www.eventbrite.com/xml/";

        private User _user;

        public EventbriteContext(string appKey, string userKey = null)
        {
            this.AppKey = appKey;
            if (userKey != null)
            {
                this.UserKey = userKey;
                _user = new User(this);
            }
        }

        public Organizer GetOrganizer(int id)
        {
            return new Organizer(id, this);
        }

        public Event GetEvent(int id)
        {
            return new EventRequest(id, this).GetResponse();
        }

        public User User
        {
            get
            {
                return _user;
            }

        }

    }
}
