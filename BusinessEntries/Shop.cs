using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Shop
    {
        public string name { get; set; }

        public decimal latitude { get; set; }
        public decimal longitude { get; set; }

        public int minOrderLimit { get; set; }
        public int maxOrderLimit { get; set; }
        public int currentOrderState { get; set; }

    }
}

