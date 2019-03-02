using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerEntities
{
    public class Order
    {
        public int orderNumber { get; set; }

        public string latitudeDecimal { get; set; }
        public string latitudeInteger { get; set; }

        public string longitudeDecimal { get; set; }
        public string longitudeInteger { get; set; }
    }
}
