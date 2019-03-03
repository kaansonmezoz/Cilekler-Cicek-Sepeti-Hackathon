using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using FileOperations.Excel.Entities;

namespace FileOperations.Excel
{
    public class ExcelParser
    {

        public List<Worksheet> parseWorkbookXml(string workbookPath)
        {   
            List<Worksheet> worksheetList = new List<Worksheet>();

            XmlDocument workbookXml = new XmlDocument();
            workbookXml.Load(workbookPath);

            XmlNodeList nodeList = workbookXml.GetElementsByTagName("sheet");

            for (int i = 0; i < nodeList.Count; i++)
            {
                worksheetList.Add(new Worksheet
                {
                    name = nodeList[i].Attributes["name"].Value,
                    id = Convert.ToInt32(nodeList[i].Attributes["sheetId"].Value)
                });
            }

            return worksheetList;
        }

        public List<Row> parseSheetXml(string sheetPath)
        {
            List<Row> rowList = new List<Row>();

            XmlDocument worksheetList = new XmlDocument();
            worksheetList.Load(sheetPath);

            XmlNodeList rowNodeList = worksheetList.GetElementsByTagName("row");           

            for (var i = 0; i < rowNodeList.Count; i++)
            {
                Row row = new Row();
                row.cellList = new List<Cell>();
                row.id = Convert.ToString(i);

                XmlNodeList childNodeList = rowNodeList[i].ChildNodes;

                for (var j = 0; j < childNodeList.Count; j++)
                {
                    Console.WriteLine(childNodeList.Item(j).ChildNodes.Item(0).InnerXml);

                    row.cellList.Add(new Cell
                    {
                        stringIndex = Convert.ToInt32(childNodeList[j].ChildNodes.Item(0).InnerXml)
                    });
                }

                rowList.Add(row);

            }

            return rowList;
        }



        public List<string> parseSharedStringsXml(string sharedStringsPath)
        {
            List<string> valueList = new List<string>();

            XmlDocument sharedStringsXml = new XmlDocument();
            sharedStringsXml.Load(sharedStringsPath);
                
            XmlNodeList nodeList = sharedStringsXml.GetElementsByTagName("si");


            for (int i = 0; i < nodeList.Count; i++)
            {  
                XmlNode node = nodeList[i].FirstChild;

                valueList.Add(node.InnerXml);
                
            }


            return valueList;
        }


        public void mapCellValues(List<Row> rowList, List<string> valueList)
        {
            foreach (Row row in rowList)
            {
                foreach (Cell cell in row.cellList)
                {
                    cell.value = valueList[Convert.ToInt32(cell.stringIndex)].Replace(".", ",");
                    Console.WriteLine(cell.value);
                }
            }
        }
    }

}


