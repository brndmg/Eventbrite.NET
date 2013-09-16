namespace EventbriteNET.Entities
{
    public class Barcode : EntityBase

    {
        public Barcode(EventbriteContext context): base(context) { }


        public string Id;
        public string Status;
    }
}