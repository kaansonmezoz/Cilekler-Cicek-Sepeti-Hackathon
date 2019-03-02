using System;
using System.Collections.Generic;
using System.Text;

namespace FileOperations.Excel.Entities
{
    public class Row
    {
        public string id { get; set; }
        public List<Cell> cellList { get; set; }

    }
}
