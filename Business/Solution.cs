using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class Solution
    {
        const int RED_SHOP_MIN_LIMIT = 20;
        const int RED_SHOP_MAX_LIMIT = 30;
        const int GREEN_SHOP_MIN_LIMIT = 35;
        const int GREEN_SHOP_MAX_LIMIT = 50;
        const int BLUE_SHOP_MIN_LIMIT = 20;
        const int BLUE_SHOP_MAX_LIMIT = 80;
        const int SUM_MIN_LIMIT = RED_SHOP_MIN_LIMIT + GREEN_SHOP_MIN_LIMIT + BLUE_SHOP_MIN_LIMIT;


        public List<Shop> shopList;
        public List<Order> orderList;
        public List<OrderCase> orderCases = new List<OrderCase>();
        public Dictionary<int, string> assignedOrders = new Dictionary<int, string>();


        public Solution(List<Shop> shopList, List<Order> orderList)
        {
            this.shopList = shopList;
            this.orderList = orderList;
        }

        public void assignLimitsOfShops()
        {
            shopList[0].minOrderLimit = 20;
            shopList[0].maxOrderLimit = 30;
            shopList[1].minOrderLimit = 35;
            shopList[1].maxOrderLimit = 50;
            shopList[2].minOrderLimit = 20;
            shopList[2].maxOrderLimit = 80;
        }


        public void mapProcessedOrders()
        {

            foreach (Order order in orderList)
            {
                foreach (Shop shop in shopList)
                {
                    decimal distance = calculateDistance(shop, order);
                    order.distances.Add(shop.name, distance);

                    orderCases.Add(new OrderCase
                    {
                        shop = shop,
                        orderNumber = order.orderNumber,
                        distance = calculateDistance(shop, order)
                    });
                }
            }


            //orderCases.OrderBy(c => c.distance).ToList();
            orderCases.Sort((case1, case2) => case1.distance < case2.distance ? -1 : 1);

            int leftOrderNumber = orderList.Count;

            for (int i = 0; i < orderCases.Count; i++)
            {
                OrderCase orderCase = orderCases[i];

                Console.WriteLine(orderCase.shop.name + "//" + orderCase.orderNumber + " //" + orderCase.distance);
                Console.WriteLine();

                if (assignedOrders.ContainsKey(orderCase.orderNumber) == false)
                {

                    if (orderCase.shop.currentOrderState < orderCase.shop.maxOrderLimit && leftOrderNumber > (SUM_MIN_LIMIT - orderCase.shop.maxOrderLimit))
                    {
                        assignedOrders.Add(orderCase.orderNumber, orderCase.shop.name);
                        leftOrderNumber--;
                        orderCase.shop.currentOrderState = orderCase.shop.currentOrderState + 1;
                    }

                }
            }

            Console.WriteLine("   ");
        }

        public decimal calculateDistance(Shop shop, Order order)
        {
            //decimal distance =  Math.Pow(Math.Pow(shop.latitude - order.latitude, 2) + Math.Pow(shop.longitude - order.longitude, 2), 0.5);
            decimal latitudeDistancePow = (shop.latitude - order.latitude) * (shop.latitude - order.latitude);
            decimal longitudeDistancePow = (shop.longitude - order.longitude) * (shop.longitude - order.longitude);


            decimal distance = (decimal)Math.Pow((double)(latitudeDistancePow + longitudeDistancePow), 0.5);
            return distance;
        }

        public Dictionary<int, string> getAssignedOrders()
        {
            return assignedOrders;
        }
    }
}














