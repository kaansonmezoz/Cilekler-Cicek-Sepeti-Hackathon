using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using FileOperations.Excel;

namespace FileOperations
{
    public class FileOperations
    {
        public void UnZip(string inputPath, string outputPath)
        {
            Console.WriteLine("Unzip process started: " + inputPath);

            try
            {
                ZipFile.ExtractToDirectory(inputPath, outputPath);
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.StackTrace);
            }


            Console.WriteLine("Unzip process finished: " + outputPath);


        }

        public void DeleteFiles(string path)
        {
            string zipPath = path + Path.DirectorySeparatorChar + "_temp_";

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

        }


    }
}