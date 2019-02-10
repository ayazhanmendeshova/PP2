using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Ayazhan Mendeshova\Desktop\PP2\task1.txt");//считываем с текстого файла 

            foreach (string line in lines)
            {
                char[] charArray = line.ToCharArray();
                for (int i = 0; i < 1; i++)
                {
                    Array.Reverse(charArray);//рассматриваем слова от начала до конца и наоборот 
                    bool a = charArray.SequenceEqual(line);
                    while (a == true)
                    {
                        Console.WriteLine(line);// если слово является палиндромом, то выводим yes
                        Console.WriteLine("YES");
                        break;
                    }
                    while (a == false)
                    {
                        Console.WriteLine(line);// если слово не является, то выводим no
                        Console.WriteLine("NO");
                        break;
                    }

                }
            }
        }
    }
}