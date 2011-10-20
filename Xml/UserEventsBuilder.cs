using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventbriteNET.Entities;
using System.IO;
using System.Xml;

namespace EventbriteNET.Xml
{
    class UserEventsBuilder : BuilderBase
    {
        public UserEventsBuilder(EventbriteContext context) : base(context) { }

        public Event[] Build(string xmlString)
        {
            this.Validate(xmlString);

            var stringReader = new StringReader(xmlString);

            var toReturn = new List<Event>();

            var doc = new XmlDocument();
            doc.LoadXml(xmlString);

            var events = doc.GetElementsByTagName("event");
            var builder = new EventBuilder(this.Context);
            foreach (XmlNode eventNode in events)
            {
                var eventEntity = builder.Build(eventNode.OuterXml);
                toReturn.Add(eventEntity);
            }

            return toReturn.ToArray();
        }
    }
}
