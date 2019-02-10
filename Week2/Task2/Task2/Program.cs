using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task2
{
    class Program
    {
      //создаем функцию, чтоб проверить является ли число простым
        
            public static bool IsPrime(int a)
            {
                if (a == 0) 
                return false;
                if (a == 1)
                    return false;
                for (int i=2; i <= Math.Sqrt(a); i++)
                {
                    if (a % i == 0)
                        return false;
                }
                return true;
            }
        static void Main(string[] args)
        { 
            StreamReader sr = new StreamReader(@"C:\Users\Ayazhan Mendeshova\Desktop\PP2\input.txt");//считываю тектовый файл, где написаны числа
            string s = sr.ReadToEnd();
            sr.Close();
            string[] str = s.Split(' ');
            int[] arr = new int[10000];
            for (int i = 0; i < str.Length; i++)
            {
                arr[i] = int.Parse(str[i]);
            }
            StreamWriter sw = new StreamWriter(@"C:\Users\Ayazhan Mendeshova\Desktop\PP2\output.txt");//в данным файле будут выведены простые числа
            for (int i = 0; i < s.Length; i++)
            {
                if (IsPrime (arr[i]))
                sw.Write(arr[i] + " ");


            }
            sw.Close();
            Console.ReadKey();

        }
    }
}
