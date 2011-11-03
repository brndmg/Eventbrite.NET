using System;
using System.Xml;
using EventbriteNET.Entities;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace EventbriteNET.Xml
{
    class EventBuilder : BuilderBase
    {
        public EventBuilder(EventbriteContext context) : base(context) { }

        public Event Build(string xmlString)
        {
            this.Validate(xmlString);

            var toReturn = new Event(this.Context);

            var doc = new XmlDocument();
            doc.LoadXml(xmlString);

            toReturn.Id = long.Parse(doc.GetElementsByTagName("id")[0].InnerText);
            toReturn.Title = doc.GetElementsByTagName("title")[0].InnerText;
            toReturn.Status = doc.GetElementsByTagName("status")[0].InnerText;
            toReturn.Description = FixEntities(CleanWordHtml(doc.GetElementsByTagName("description")[0].InnerText));
            toReturn.Url = doc.GetElementsByTagName("url")[1].InnerText;
            toReturn.StartDateTime = DateTime.Parse(doc.GetElementsByTagName("start_date")[0].InnerText);
            toReturn.EndDateTime = DateTime.Parse(doc.GetElementsByTagName("end_date")[0].InnerText);
            toReturn.Timezone = doc.GetElementsByTagName("timezone")[0].InnerText;
            toReturn.Created = DateTime.Parse(doc.GetElementsByTagName("created")[0].InnerText);
            toReturn.Modified = DateTime.Parse(doc.GetElementsByTagName("modified")[0].InnerText);


            //TICKETS
            var tickets = doc.GetElementsByTagName("ticket");
            var builder = new TicketBuilder(this.Context);
            foreach (XmlNode ticketNode in tickets)
            {
                var ticket = builder.Build(ticketNode.OuterXml);
                toReturn.Tickets.Add(ticket.Id, ticket);
            }
            
            //VENUE
            var venue = doc.GetElementsByTagName("venue")[0];
            var venueBuilder = new VenueBuilder(this.Context);
            toReturn.Venue = venueBuilder.Build(venue.OuterXml);

            return toReturn;
        }


       

        static string CleanWordHtml(string html)
        {
            StringCollection sc = new StringCollection();
            // get rid of unnecessary tag spans (comments and title)
            sc.Add(@"<!--(\w|\W)+?-->");
            sc.Add(@"<title>(\w|\W)+?</title>");
            // Get rid of classes and styles
            sc.Add(@"\s?class=\w+");
            sc.Add(@"\s+style='[^']+'");
            // Get rid of unnecessary tags
            sc.Add(@"<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|body|/?body|/?span|!\[)[^>]*?>");
            // Get rid of empty paragraph tags
            sc.Add(@"(<[^>]+>)+&nbsp;(</\w+>)+");
            // remove bizarre v: element attached to <img> tag
            sc.Add(@"\s+v:\w+=""[^""]+""");
            // remove extra lines
            sc.Add(@"(\n\r){2,}");
            foreach (string s in sc)
            {
                html = Regex.Replace(html, s, "", RegexOptions.IgnoreCase);
            }
            return html;
        }

        static string FixEntities(string html)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("â€œ", "&ldquo;");
            nvc.Add("â€", "&rdquo;");
            nvc.Add("Ã¢â‚¬â€œ", "&mdash;");
            nvc.Add("Â", "");
            nvc.Add("â€™", "'");
            
            foreach (string key in nvc.Keys)
            {
                html = html.Replace(key, nvc[key]);
            }
            return html;
        }
    }
}
