using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using FileOperations.Excel;

namespace FileOperations
{
    public class FileOperations
    {
        public void readFile(string folderPath, string fileName)
        {
            string fileExtension = fileName.Split('.')[1];

            if (fileExtension != null)
            {      // TODO:Buraya bir factory pattern cakariz guzel olur. Tepede bir FileReader interface'i olur altında dosyalara gore farklilik gosteren islemler olur. İlgili dosya extension'ina gore ilgili dosya isi cagirilir
                if (fileExtension.CompareTo("xlsx") == 0)
                {
                    ExcelReader excelReader = new ExcelReader();

                    //TODO: excelReader.readExceFile(folderPath + Path.DirectorySeparatorChar + fileName);
                }
                else
                {
                    Console.WriteLine(fileExtension);
                    Console.WriteLine("Hata: Desteklenmeyen dosya uzantısı !");
                    Console.WriteLine("Lütfen xlsx uzantılı dosya giriniz.");
                }
            }
            else
            {
                Console.WriteLine("Dosya adini uzantisiyla birlikte girin");
            }
        }

        public void UnZip(string inputPath, string outputPath)
        {
            Console.WriteLine("Unzip process started: " + inputPath);

            //ZipFile.CreateFromDirectory(inputPath, outputPath);
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