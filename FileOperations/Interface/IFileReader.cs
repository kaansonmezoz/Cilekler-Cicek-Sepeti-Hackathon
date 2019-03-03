using FileOperations.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileOperations.Interface
{
    public interface IFileReader
    {
        ReadFileEntity ReadFile(string fileFolder, string fileName);
    }
}
