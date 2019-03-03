using System;

namespace ControllerEntities
{
    public class Shop
    {
        public string name { get; set; }

        public string latitudeDecimal { get; set; }
        public string latitudeInteger { get; set; }

        public string longitudeDecimal { get; set; }
        public string longitudeInteger { get; set; }

        public int maxOrderLimit { get; set; }
        public int minOrderLimit { get; set; }
    }
}
