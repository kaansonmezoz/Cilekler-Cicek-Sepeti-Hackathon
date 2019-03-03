using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileOperations.Entity
{
    public class ReadFileEntity
    {
        public List<Order> orderList { get; set; }
        public List<Shop> shopList   { get; set; }
    }
}
