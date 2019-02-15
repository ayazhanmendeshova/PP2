using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task1
{
    class FarManager //создание необходимых переменных
    {
        public bool dir;
        public int cursor;
        public string path;
        public int size;
        public bool hidden;
        DirectoryInfo direc = null;
        FileSystemInfo currentFs = null;

        public FarManager() // создадим конструктор, чтобы получить курсор на 0-ю строку, если откроем новую папку
        {
            cursor = 0;
        }

        public FarManager(string path) //конструктор для всех методов
        {
            this.path = path;
            cursor = 0;
            dir = true;
            direc = new DirectoryInfo(path);
            size = direc.GetFileSystemInfos().Length;
            hidden = true;
        }

        public void Color(FileSystemInfo fs, int indx) //способ раскрасить папку, файлы и представить строку в разные цвета
        {
            if (cursor == indx)
            {
                currentFs = fs;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (fs.GetType() == typeof(FileInfo))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        public void Korset() //метод сортировки и записи всех папок и файлов
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            var folders = direc.GetDirectories();
            var files = direc.GetFiles();

            int j = 0;
            foreach (var i in folders)
            {
                if (hidden == false && (i.Name.StartsWith(".") || i.Name.StartsWith("$")))
                {
                    continue;
                }
                Color(i, j);
                j++;
                Console.WriteLine(j + ". " + i.Name);
            }

            int temp = j;
            foreach (var i in files)
            {
                if (hidden == false && (i.Name.StartsWith(".") || i.Name.StartsWith("$")))
                {
                    continue;
                }
                Color(i, temp);
                temp++;
                Console.WriteLine(temp + ". " + i.Name);
            }
        }

        public void UA() //если нажать стрелку вверх - пойдет вверх по списку
        {
            cursor--;
            if (cursor < 0)
                cursor = size - 1;
        }

        public void DA() //а это если вниз
        {
            cursor++;
            if (cursor == size)
                cursor = 0;
        }

        public void CalcS() // пересчитать размер каждый раз, когда мы скрываем скрытые файлы
        {
            direc = new DirectoryInfo(path);
            FileSystemInfo[] fs = direc.GetFileSystemInfos();
            size = direc.GetFileSystemInfos().Length;

            for (int i = 0; i < direc.GetFileSystemInfos().Length; i++)
            {
                if ((fs[i].Name[0] == '.' || fs[i].Name[0] == '$') && hidden == false)
                    size--;
            }

        }

        public void Rabotay()
        {
            ConsoleKeyInfo cki;
            do
            {
                if (dir) //если мы в dir, пересчитываем размер
                {
                    CalcS();
                }

                Korset();

                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.UpArrow) //вызываем метод
                    UA();

                if (cki.Key == ConsoleKey.DownArrow) 
                    DA();

                if (cki.Key == ConsoleKey.PageDown) //вызываем метод - скрытые файлы
                {
                    hidden = false;
                    cursor = 0;
                }

                if (cki.Key == ConsoleKey.PageUp) //вызываем метод - показывать скрытые файлы
                {
                    hidden = true;
                    cursor = 0;
                }

                if (cki.Key == ConsoleKey.Enter) //открыть папку или файл
                {
                    cursor = 0;
                    if (currentFs.GetType() == typeof(DirectoryInfo))
                    {
                        path = currentFs.FullName;
                    } // для папки

                    else
                    {
                        path = currentFs.FullName;
                        Console.Clear();
                        string str;
                        dir = false;
                        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);
                        str = sr.ReadToEnd();
                        Console.WriteLine(str);
                        Console.ReadKey();
                        sr.Close();
                        fs.Close();
                    } // для файла
                }

                if (cki.Key == ConsoleKey.Backspace)
                {

                    cursor = 0;
                    path = direc.Parent.FullName;
                    dir = true;
                } // чтобы вернуться к предыдущей папке

                if (cki.Key == ConsoleKey.Delete)
                {
                    if (currentFs.GetType() == typeof(DirectoryInfo))
                        Directory.Delete(currentFs.FullName);
                    else
                        File.Delete(currentFs.FullName);
                } //удаление файла или папки


                if (cki.Key == ConsoleKey.LeftArrow)
                {
                    Console.Clear();
                    string nname = Console.ReadLine();

                    string npath = Path.Combine(direc.FullName, nname);
                    if (currentFs.GetType() == typeof(FileInfo))
                    {
                        File.Move(currentFs.FullName, npath);
                    }
                    else
                    {
                        Directory.Move(currentFs.FullName, npath);
                    }
                } // переименование файла или папки
            } while (cki.Key != ConsoleKey.Escape); //чтобы выйти из программы
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Ayazhan Mendeshova\Desktop\PP2"; //папка котораю собираюсь открывать
            FarManager fm = new FarManager(path);
            fm.Rabotay(); // вызывающий метод

            Console.ReadKey();
        }
    }
}