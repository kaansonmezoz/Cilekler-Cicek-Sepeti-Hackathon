using BusinessEntities;
using FileOperations.Entity;
using FileOperations.Excel.Entities;
using FileOperations.Interface;
using System;
using System.Collections.Generic;
using System.IO;


namespace FileOperations.Excel
{
    public class ExcelReader : IFileReader
    {
        private const int INDEX = 0;
        private const int LAT_INDEX = 1;
        private const int LON_INDEX = 2;

        public ReadFileEntity ReadFile(string fileFolder, string fileName, string entity) {
            string zipPath = fileFolder + Path.DirectorySeparatorChar + "_temp_";

            FileOperations fileOperations = new FileOperations();
            fileOperations.UnZip(fileFolder + Path.DirectorySeparatorChar + fileName, zipPath);

            List<Worksheet> worksheet = extractWorkbook(zipPath);

            List<string> sharedString = extractSharedString(zipPath);

            List<Row> orderRowList = null;
            List<Row> shopRowList = null;

            ExcelParser parser = new ExcelParser();

            if (entity.Equals("shop") == true)
            {
                shopRowList = extractWorksheet(zipPath, worksheet[1].id);
                parser.mapCellValues(shopRowList, sharedString);
            }
            else if (entity.Equals("order") == true)
            {
                orderRowList = extractWorksheet(zipPath, worksheet[0].id);
                parser.mapCellValues(orderRowList, sharedString);
            }
            else {
                fileOperations.DeleteFiles(zipPath);
                // boyle bir entity yok tarzinda bir sey vermek lazim
                return null;
            }

            fileOperations.DeleteFiles(zipPath);

            return new ReadFileEntity{
                orderList = createOrderList(orderRowList),
                shopList = createShopList(shopRowList)
            };
        }

        private List<Worksheet> extractWorkbook(string zipOutputPath)
        {
            string workbookFolder = zipOutputPath + Path.DirectorySeparatorChar + "xl";
            string workbookFileName = "workbook.xml";

            ExcelParser operationsHelper = new ExcelParser();

            List<Worksheet> worksheetList = operationsHelper.parseWorkbookXml(workbookFolder + Path.DirectorySeparatorChar + workbookFileName);

            return worksheetList;
        }


        private List<Row> extractWorksheet(string zipOutputPath, int worksheetId)
        {
            string worksheetFolder = zipOutputPath + Path.DirectorySeparatorChar + "xl" + Path.DirectorySeparatorChar + "worksheets";
            string worksheetFileName = "sheet" + worksheetId + ".xml";

            ExcelParser excelHelper = new ExcelParser();

            return excelHelper.parseSheetXml(worksheetFolder + Path.DirectorySeparatorChar + worksheetFileName);
        }

        private List<string> extractSharedString(string zipOutputPath)
        {
            string sharedStringsFolder = zipOutputPath + Path.DirectorySeparatorChar + "xl";
            string sharedStringsFileName = "sharedStrings.xml";

            ExcelParser operationsHelper = new ExcelParser();

            return operationsHelper.parseSharedStringsXml(sharedStringsFolder + Path.DirectorySeparatorChar + sharedStringsFileName);
        }

        public List<Order> createOrderList(List<Row> orderListRows)
        {
            if (orderListRows == null) {
                return null;
            }

            List<Order> orderList = new List<Order>();

            Console.WriteLine("orderListRows.Count: " + orderListRows.Count);

            for (int i = 1; i < orderListRows.Count; i++)
            {
                List<Cell> cellList = orderListRows[i].cellList;

                Console.WriteLine("orderNumber: " + cellList[INDEX].value);
                Console.WriteLine("latitude: " + cellList[LAT_INDEX].value);
                Console.WriteLine("longitude: " + cellList[LON_INDEX].value);

                orderList.Add(new Order
                {
                    orderNumber = Convert.ToInt32(cellList[INDEX].value),
                    latitude = cellList[LAT_INDEX].value,
                    longitude = cellList[LON_INDEX].value
                });
            }

            return orderList;
        }

        public List<Shop> createShopList(List<Row> shopListRows)
        {
            if (shopListRows == null) {
                return null;
            }

            List<Shop> shopList = new List<Shop>();

            Console.WriteLine(shopListRows.Count);

            int[] minOrderLimit = { 20, 35, 20 };  // Buralar pek hos olmadi ama ...
            int[] maxOrderLimit = { 30, 50, 80 };

            for (int i = 1; i < shopListRows.Count; i++)
            {
                List<Cell> cellList = shopListRows[i].cellList;

                shopList.Add(new Shop
                {
                    name = cellList[INDEX].value,
                    latitude = cellList[LAT_INDEX].value,
                    longitude = cellList[LON_INDEX].value,

                    minOrderLimit = minOrderLimit[i - 1],
                    maxOrderLimit = maxOrderLimit[i - 1]
                });
            }

            return shopList;
        }
    }
}