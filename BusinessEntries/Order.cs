using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class Order
    {
        public int orderNumber { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public Dictionary<string, decimal> distances = new Dictionary<string, decimal>();
    }
}
