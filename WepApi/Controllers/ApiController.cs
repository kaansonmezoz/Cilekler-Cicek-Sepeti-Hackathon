using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessEntities;
using FileOperations.Excel;
using FileOperations;
using FileOperations.Excel.Entities;
using Microsoft.AspNetCore.Mvc;
using FileOperations.Interface;
using FileOperations.Factory;
using FileOperations.Entity;

namespace Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [Route("shops")]
        [HttpGet]
        public List<ControllerEntities.Shop> GetShops()
        {
            string absolutePath = Directory.GetCurrentDirectory();
            string fileFolder = absolutePath + Path.DirectorySeparatorChar + "data"; 
            string fileName = "siparis_ve_bayi_koordinatlari.xlsx";

            FileReaderFactory factory = new FileReaderFactory();
            IFileReader reader = factory.createFileReader(fileName.Split('.')[1]);

            ReadFileEntity fileEntity = reader.ReadFile(fileFolder, fileName, "shop");

            return fileEntity.shopList.Select((shop) => new ControllerEntities.Shop
            {
                name = shop.name,
                
                latitudeInteger = Convert.ToString(shop.latitude).Split(",")[0],
                latitudeDecimal = Convert.ToString(shop.latitude).Split(",")[1],

                longitudeInteger = Convert.ToString(shop.longitude).Split(",")[0],
                longitudeDecimal = Convert.ToString(shop.longitude).Split(",")[1],

                minOrderLimit = shop.minOrderLimit,
                maxOrderLimit = shop.maxOrderLimit
            }).ToList();
        }


        [Route("orders")]
        [HttpGet]
        public List<ControllerEntities.Order> GetOrders()
        {
            string absolutePath = Directory.GetCurrentDirectory();
            string fileFolder = absolutePath + Path.DirectorySeparatorChar + "data";
            string fileName = "siparis_ve_bayi_koordinatlari.xlsx";

            FileReaderFactory factory = new FileReaderFactory();
            IFileReader reader = factory.createFileReader(fileName.Split('.')[1]);

            ReadFileEntity fileEntity = reader.ReadFile(fileFolder, fileName, "order");

            return fileEntity.orderList.Select((order) => new ControllerEntities.Order{
                orderNumber = order.orderNumber,

                latitudeInteger = Convert.ToString(order.latitude).Split(",")[0],
                latitudeDecimal = Convert.ToString(order.latitude).Split(",")[1],

                longitudeInteger = Convert.ToString(order.longitude).Split(",")[0],
                longitudeDecimal = Convert.ToString(order.longitude).Split(",")[1],

            }).ToList();
        }

    }
}