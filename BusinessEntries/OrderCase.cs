using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class OrderCase
    {
        public Shop shop { get; set; }
        public int orderNumber { get; set; }
        public decimal distance { get; set; }

    }
}
