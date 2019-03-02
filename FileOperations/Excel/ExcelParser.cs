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
        {   // Todo: burada da factory pattern uygulanabilir parse etme islemi benzer olacak handler kismi falan filan sadece alinacak attribute'lar hangi xml dosyasi icerisinden alinacagina bagli olarak. hani loglama dosyalarinda yapmistik ya onun gibi bir sey yapilabilir.
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

                Console.WriteLine("------ Attributes -------");
                Console.WriteLine(worksheetList[i].id);
                Console.WriteLine(worksheetList[i].name);
            }

            return worksheetList;
        }

        public List<Row> parseSheetXml(string sheetPath)
        {
            List<Row> rowList = new List<Row>();

            Console.WriteLine("Reading file:" + sheetPath);

            XmlDocument worksheetList = new XmlDocument();
            worksheetList.Load(sheetPath);

            XmlNodeList rowNodeList = worksheetList.GetElementsByTagName("row");
            Console.WriteLine("XmlNodeList rowNodeList = worksheetList.GetElementsByTagName(row); ");
            Console.WriteLine("Count: " + rowNodeList.Count);

            for (var i = 0; i < rowNodeList.Count; i++)
            {
                Row row = new Row();
                row.cellList = new List<Cell>();
                row.id = Convert.ToString(i);

                XmlNodeList childNodeList = rowNodeList[i].ChildNodes;

                //Console.WriteLine("----------- Inner Text -------");
                //Console.WriteLine(rowNodeList[i].InnerXml);

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
            {  //Todo: burada degerler basilinca null geliyor garip
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
                }
            }
        }
    }

}


