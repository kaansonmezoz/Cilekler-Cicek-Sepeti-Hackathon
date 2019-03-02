using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business;
using BusinessEntities;
using FileOperations.Excel;
using FileOperations.Excel.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        //TODO: Olmayan route'lar icin de bir error handling yazmak lazim
        //TODO: error handling falan gibi şeyler de eklesek fena olmaz

        [Route("shops")]
        [HttpGet]
        public List<ControllerEntities.Shop> GetShops()
        {
            const int SHOPS_SHEET_NUMBER = 1;

            string absolutePath = Directory.GetCurrentDirectory();
            // Main'in yerini verir.
            string fileFolder = absolutePath + Path.DirectorySeparatorChar + "data"; // TODO: Bunlar disaridan parametre alacak seklinde
            string fileName = "siparis_ve_bayi_koordinatlari.xlsx";  // TODO: Duzeltilecek

            ExcelReader excelReader = new ExcelReader();

            List<Row> shopListRows = excelReader.readFile(fileFolder, fileName, SHOPS_SHEET_NUMBER).rowList;

            List<Shop> shopList = excelReader.createShopList(shopListRows);

            Console.WriteLine(shopList[0].latitude);

            return shopList.Select((shop) => new ControllerEntities.Shop
            {
                name = shop.name,
                
                latitudeInteger = Convert.ToString(shop.latitude).Split(",")[0],
                latitudeDecimal = Convert.ToString(shop.latitude).Split(",")[1],

                longitudeInteger = Convert.ToString(shop.longitude).Split(",")[0],
                longitudeDecimal = Convert.ToString(shop.longitude).Split(",")[1],
            }).ToList();
        }


        [Route("result")]
        [HttpGet]
        //TODO: bir tane de 
        public Dictionary<int, string> GetResult()
        {
            const int ORDERS_SHEET_NUMBER = 0;
            const int SHOPS_SHEET_NUMBER = 1;

            string absolutePath = Directory.GetCurrentDirectory();
            // Main'in yerini verir.
            string fileFolder = absolutePath + Path.DirectorySeparatorChar + "data"; // TODO: Bunlar disaridan parametre alacak seklinde
            string fileName = "siparis_ve_bayi_koordinatlari.xlsx";  // TODO: Duzeltilecek

            ExcelReader excelReader = new ExcelReader();

            List<Row> orderListRows = excelReader.readFile(fileFolder, fileName, ORDERS_SHEET_NUMBER).rowList;

            List<Order> orderList = excelReader.createOrderList(orderListRows);

            List<Row> shopListRows = excelReader.readFile(fileFolder, fileName, SHOPS_SHEET_NUMBER).rowList;

            List<Shop> shopList = excelReader.createShopList(shopListRows);

            Solution solution = new Solution(shopList, orderList);

            solution.shopList = shopList;

            solution.assignLimitsOfShops();
            solution.mapProcessedOrders();


            Dictionary<int, string> assignedOrders = solution.getAssignedOrders();

            Console.WriteLine(string.Join("\n", assignedOrders.Select(x => x.Key + "  --->  " + x.Value).ToArray()));
            Console.WriteLine("   ");

            return assignedOrders;
        }

        [Route("orders")]
        [HttpGet]
        public List<ControllerEntities.Order> GetOrders()
        {
            const int ORDERS_SHEET_NUMBER = 0;

            string absolutePath = Directory.GetCurrentDirectory();
            // Main'in yerini verir.
            string fileFolder = absolutePath + Path.DirectorySeparatorChar + "data"; // TODO: Bunlar disaridan parametre alacak seklinde
            string fileName = "siparis_ve_bayi_koordinatlari.xlsx";  // TODO: Duzeltilecek

            ExcelReader excelReader = new ExcelReader();

            List<Row> orderListRows = excelReader.readFile(fileFolder, fileName, ORDERS_SHEET_NUMBER).rowList;

            List<Order> orderList = excelReader.createOrderList(orderListRows);

            return orderList.Select((order) => new ControllerEntities.Order{
                orderNumber = order.orderNumber,

                latitudeInteger = Convert.ToString(order.latitude).Split(",")[0],
                latitudeDecimal = Convert.ToString(order.latitude).Split(",")[1],

                longitudeInteger = Convert.ToString(order.longitude).Split(",")[0],
                longitudeDecimal = Convert.ToString(order.longitude).Split(",")[1],

            }).ToList();
        }

    }
}