using FileOperations.Excel;
using FileOperations.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileOperations.Factory
{
    public class FileReaderFactory
    {
        public IFileReader createFileReader(string fileExtension) {
            if (fileExtension.Equals("xlsx") == true) {
                return new ExcelReader();
            }

            return null;
        }
    }
}
