using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class Order
    {
        public int orderNumber { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Dictionary<string, decimal> distances = new Dictionary<string, decimal>();
    }
}
