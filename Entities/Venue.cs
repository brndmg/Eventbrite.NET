using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventbriteNET.Entities
{
    /// <summary>
    /// venue
    /// The event venue with the following structure:     
    /// </summary>
    public class Venue : EntityBase
    {
        
        public Venue(EventbriteContext context)
            : base(context)
        {
        }

        /// <summary>
        ///id integer
        ///The venue ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name string - The venue name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// address string - The venue address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// address_2 string - The venue address (continued).
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// city string - The venue city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// region string - The venue state/province/county depending on the country.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// postal_code string - The venue postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// country string - The venue country name.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// country_code string - The venue country code, in 2-letter ISO 3166 format (e.g., “US”).
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// longitude float - The venue GeoLocation in WGS84 (Longitude).
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// latitude float - The venue GeoLocation in WGS84 (Latitude).
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Lat-Long string - The venue GeoLocation in WGS84 (Latitude/Longitude).
        /// </summary>
        public string LatLong { get; set; }

        public override string ToString()
        {
            var address = Address + (!String.IsNullOrEmpty(Address2) ? "," + Address2 : String.Empty);
            var cityregion = City + (!String.IsNullOrEmpty(Region) ? "," + Region : String.Empty);
            return String.Format("{0} {1} {2} {3}", Name, address, cityregion, CountryCode);
        }
    }
}