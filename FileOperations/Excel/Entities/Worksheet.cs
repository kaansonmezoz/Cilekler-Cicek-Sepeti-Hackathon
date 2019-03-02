using System;
using System.Collections.Generic;
using System.Text;

namespace FileOperations.Excel.Entities
{
    public class Worksheet
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Row> rowList { get; set; }
    }
}