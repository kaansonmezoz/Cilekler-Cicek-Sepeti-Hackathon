using BusinessEntities;
using FileOperations.Excel.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileOperations.Excel
{
    public class ExcelReader
    {
        private const int INDEX = 0;
        private const int LAT_INDEX = 1;
        private const int LON_INDEX = 2;

        public Worksheet readFile(string fileFolder, string fileName, int sheetNumber)
        { //Windows Formatındaki excel dosyalarini okur
            string zipPath = fileFolder + Path.DirectorySeparatorChar + "_temp_";

            FileOperations fileOperations = new FileOperations();
            fileOperations.UnZip(fileFolder + Path.DirectorySeparatorChar + fileName, zipPath);

            List<Worksheet> worksheet = extractWorkbook(zipPath);

            Console.WriteLine("List<Worksheet> worksheet = extractWorkbook(zipPath);");
            Console.WriteLine("Size of " + worksheet.Count);

            //TODO: bu alayi icin yapilmasi gerekiyor.
            List<Row> rowList = extractWorksheet(zipPath, worksheet[sheetNumber].id);
            worksheet[sheetNumber].rowList = rowList;

            Console.WriteLine("List<Row> rowList = extractWorksheet(zipPath, worksheet[sheetNumber].id);");
            Console.WriteLine("Size of " + rowList.Count);

            List<string> sharedString = extractSharedString(zipPath);

            Console.WriteLine("List<string> sharedString = extractSharedString(zipPath);");
            Console.WriteLine("Size of " + sharedString.Count);


            ExcelParser excelHelper = new ExcelParser();

            excelHelper.mapCellValues(rowList, sharedString);

            fileOperations.DeleteFiles(zipPath);

            return worksheet[sheetNumber];
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
            List<Order> orderList = new List<Order>();

            Console.WriteLine("orderListRows.Count: " + orderListRows.Count);

            for (int i = 1; i < orderListRows.Count; i++)
            {
                List<Cell> cellList = orderListRows[i].cellList;

                orderList.Add(new Order
                {
                    orderNumber = Convert.ToInt32(cellList[INDEX].value),
                    latitude = cellList[LAT_INDEX].value,
                    longitude = cellList[LON_INDEX].value,
                    distances = new Dictionary<string, decimal>()
                });
            }

            return orderList;
        }

        public List<Shop> createShopList(List<Row> shopListRows)
        {
            List<Shop> shopList = new List<Shop>();

            Console.WriteLine(shopListRows.Count);

            for (int i = 1; i < shopListRows.Count; i++)
            {
                List<Cell> cellList = shopListRows[i].cellList;

                shopList.Add(new Shop
                {
                    name = cellList[INDEX].value,
                    latitude = cellList[LAT_INDEX].value,
                    longitude = cellList[LON_INDEX].value,
                });
            }

            return shopList;
        }
    }
}