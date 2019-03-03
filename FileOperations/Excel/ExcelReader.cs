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

        public ReadFileEntity ReadFile(string fileFolder, string fileName) {
            string zipPath = fileFolder + Path.DirectorySeparatorChar + "_temp_";

            FileOperations fileOperations = new FileOperations();
            fileOperations.UnZip(fileFolder + Path.DirectorySeparatorChar + fileName, zipPath);

            List<Worksheet> worksheet = extractWorkbook(zipPath);

            // Buradan sonrasi iste iki sheet icinde calisacak sekilde yapilmali
            List<Row> orderRowList = extractWorksheet(zipPath, worksheet[0].id);
            List<Row> shopRowList = extractWorksheet(zipPath, worksheet[1].id);

            List<string> sharedString = extractSharedString(zipPath);

            mapExcelRowValues(shopRowList, orderRowList, sharedString);

            fileOperations.DeleteFiles(zipPath);

            return new ReadFileEntity{
                orderList = createOrderList(orderRowList),
                shopList  = createShopList(shopRowList)
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

        public void mapExcelRowValues(List<Row> shopRowList, List<Row> orderRowList, List<string> sharedString){

            ExcelParser excelHelper = new ExcelParser();

            excelHelper.mapCellValues(shopRowList, sharedString);
            excelHelper.mapCellValues(orderRowList, sharedString);
        }

        public List<Order> createOrderList(List<Row> orderListRows)
        {
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