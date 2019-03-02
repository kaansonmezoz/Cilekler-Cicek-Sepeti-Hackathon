using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerEntities
{
    public class AssignedOrder
    {
        public List<Order> orderList { get; set; }
        public Shop shop { get; set; }
    }
}
