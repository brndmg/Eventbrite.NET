using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EventbriteNET.Entities;

namespace EventbriteNET.Xml
{
    class BarcodeBuilder : BuilderBase
    {
        public BarcodeBuilder(EventbriteContext context) : base(context) { }

        public Barcode Build(string xmlString)
        {
            this.Validate(xmlString);

            var toReturn = new Barcode(this.Context);

            var doc = new XmlDocument();
            doc.LoadXml(xmlString);

            toReturn.Id = TryGetElementValue("id", doc);
            toReturn.Status = TryGetElementValue("status", doc);

            return toReturn;
        }
    }
}
