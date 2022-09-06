using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prakt8
{
    class Program
    {
        static void DirInfo(DirectoryInfo di)
        {   // Вывод информации о каталоге 
            Console.WriteLine("===== Directory Info=====");
            Console.WriteLine(" Full Name: " + di.FullName);
            Console.WriteLine(" Name: " + di.Name);
            Console.WriteLine(" Parent: " + di.Parent);
            Console.WriteLine(" Creation: " + di.CreationTime);
            Console.WriteLine(" Attributes: " + di.Attributes);
            Console.WriteLine(" Root: " + di.Root);
            Console.WriteLine("=========================");
        }
        static void FilesInfo(FileInfo fi)
        {   // Вывод информации о файле
            Console.WriteLine("======= File  Info=======");
            Console.WriteLine(" Name: " + fi.Name);
            Console.WriteLine(" Full Name: " + fi.FullName);
            Console.WriteLine(" Extension: " + fi.Extension);
            Console.WriteLine(" Parent: " + fi.DirectoryName);
            Console.WriteLine(" Creation: " + fi.CreationTime);
            Console.WriteLine(" Last Access: " + fi.LastAccessTime);
            Console.WriteLine(" Last Write: " + fi.LastWriteTime);
            Console.WriteLine(" Attributes: " + fi.Attributes);
            Console.WriteLine("=========================");
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Копирование каталогов
            if (source.FullName.ToLower() == target.FullName.ToLower())
                return;
            if (Directory.Exists(target.FullName) == false)
                Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.GetFiles())
            {
                //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        static void Main(string[] args)
        {
            /******************************************Создание D:\MyDir\temp**********************************/
            DirectoryInfo di1 = new DirectoryInfo(@"d:\MyDir\temp");
            try { 
                di1.Create();
                DirInfo(di1);
            }
            catch (Exception)
            { Console.WriteLine("Попытка не удалась ");
            }
            /***************************************************************************************************/
            /******************************************Копирование директорий***********************************/
            try
            {
                string sourceDirectory = @"d:\111"; //откуда
                string targetDirectory = @"d:\MyDir\temp\"; //куда

                DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

                CopyAll(diSource, diTarget);
            }
            catch (Exception e)
            {Console.WriteLine("Error: " + e.Message);}
            /***********************************************************************************************/
            /******************************************Работа с атрибутами**********************************/
            DirectoryInfo dir1 = new DirectoryInfo(@"d:\MyDir\temp\");
            if (Directory.Exists(@"d:\MyDir\temp\"))
            {
                string[] dirs = Directory.GetDirectories(@"d:\MyDir\temp");
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                    string DirName = s;
                    DirectoryInfo dir3 = new DirectoryInfo(DirName);
                    FileInfo[] files1 = dir3.GetFiles();
                    if (Directory.Exists(s))
                    {
                        int i = 0;
                        while (i<files1.Length)
                        {
                            FileAttributes attributes = File.GetAttributes(files1[0].FullName);  //получение атрибутов файла
                                                                                                 // Make the file Hidden
                            File.SetAttributes(files1[i].FullName, File.GetAttributes(files1[i].FullName) | FileAttributes.Hidden); //добавление нового атрибута
                            Console.WriteLine($"The {files1[i].FullName} file is now Hidden.", files1[i].FullName);
                            files1 = dir3.GetFiles();  //обновление информации об атрибутах
                            i++;
                        }
                    }
                }
            }
            /***********************************************************************************************/
            /******************************************Информация о файлах**********************************/
            string Directory1 = @"d:\MyDir\temp\";
            if (Directory.Exists(Directory1))
            {
                Console.WriteLine("============");
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(Directory1);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                //Console.WriteLine("======");
                //Console.WriteLine("Файлы:");
                //string[] files = Directory.GetFiles(Directory1);
                //foreach (string s in files)
                //{
                //    Console.WriteLine(s);
                //}
                Console.WriteLine("=====================");
                Console.WriteLine("Файлы в подкаталогах:");
                foreach (string s in dirs)
                {
                    string[] files2 = Directory.GetFiles(s);
                    foreach (string s1 in files2)
                        Console.WriteLine(s1);
                }
                foreach (string s in dirs)
                {
                    string DirName = s;
                    DirectoryInfo dir2 = new DirectoryInfo(DirName);
                    FileInfo[] files2 = dir2.GetFiles();
                    if (Directory.Exists(s))
                    {
                        Console.WriteLine("Скопированные файлы:");
                        foreach (FileInfo fi in files2)
                            FilesInfo(fi);
                    }
                }
            }
            /***********************************************************************************************/
            /************************************Удаление всех подкатологов*********************************/
            string[] dirs1 = Directory.GetDirectories(Directory1);
            foreach (string s in dirs1)
            {
                if (Directory.Exists(s))
                {
                    Directory.Delete(s, true);
                    Console.WriteLine($"Каталог {s} удален");
                }
                else
                {
                    Console.WriteLine("Каталог не существует");
                }
            }
            /***********************************************************************************************/
            Console.ReadKey();
        }
    }
}
