using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Task3
{
    class Program
    {
        
           
        public static void probel(int num)//создаем функцию, которая оставляет пробелы между папками
        {
            for (int i = 0; i < num; i++)
            {
                Console.Write("    ");
            }
        }

        public static void Ex(DirectoryInfo dir, int a)//создаем рекрусивную фунцию, которая выводит названия и все содержимое в папке
        {
            foreach (FileInfo f in dir.GetFiles()) // для файлов
            {
                probel(a);
                Console.WriteLine(f.Name);
            }
            foreach (DirectoryInfo d in dir.GetDirectories())//для папок
            {
                probel(a);
                Console.WriteLine(d.Name);
                Ex(d, a + 1);
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Ayazhan Mendeshova\Desktop\PP2");
            Ex(dir, 0);
        }// вызовем функцию

    }
    
}
