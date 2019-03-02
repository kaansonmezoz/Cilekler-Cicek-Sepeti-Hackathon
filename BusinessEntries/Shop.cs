using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Shop
    {
        public string name { get; set; }

        public string latitude { get; set; }
        public string longitude { get; set; }

        public int minOrderLimit { get; set; }
        public int maxOrderLimit { get; set; }
        public int currentOrderState { get; set; }

    }
}

