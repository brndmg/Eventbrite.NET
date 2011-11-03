using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventbriteNET.Entities;
using System.IO;
using System.Xml;

namespace EventbriteNET.Xml
{
    class VenueBuilder : BuilderBase
    {
        public VenueBuilder(EventbriteContext context) : base(context) { }

        public Venue Build(string xmlString)
        {
            this.Validate(xmlString);

            var stringReader = new StringReader(xmlString);

            var toReturn = new Venue(this.Context);

            var doc = new XmlDocument();
            doc.LoadXml(xmlString);

            try
            {
                toReturn.Id = long.Parse(doc.GetElementsByTagName("id")[0].InnerText);
                toReturn.Name = doc.GetElementsByTagName("name")[0].InnerText.Trim();
                toReturn.Address = doc.GetElementsByTagName("address")[0].InnerText.Trim();
                toReturn.Address2 = doc.GetElementsByTagName("address_2")[0].InnerText.Trim();
                toReturn.City = doc.GetElementsByTagName("city")[0].InnerText.Trim();
                toReturn.Region = doc.SelectSingleNode("region") != null ? doc.GetElementsByTagName("region")[0].InnerText.Trim() : String.Empty;
                toReturn.PostalCode = doc.GetElementsByTagName("postal_code")[0].InnerText.Trim();
                toReturn.Country = doc.GetElementsByTagName("country")[0].InnerText.Trim();
                toReturn.CountryCode = doc.GetElementsByTagName("country_code")[0].InnerText.Trim();
                toReturn.Longitude = decimal.Parse(doc.GetElementsByTagName("longitude")[0].InnerText.Trim());
                toReturn.Latitude = decimal.Parse(doc.GetElementsByTagName("latitude")[0].InnerText.Trim());
                toReturn.LatLong = doc.GetElementsByTagName("Lat-Long")[0].InnerText.Trim();
            }
            catch (Exception)
            {                
                //swallow it.
                //TODO: Fix this.
            }            

            return toReturn;
        }


    }
}
