using System;
using System.Text;
using System.Linq;
using System.Globalization;

namespace ConsoleApp1
{
            /*"1.Дана строка, состоящая из строчных 
               "английских букв. Заменить в ней все буквы,стоящие после гласных,
               " на следующие по алфавиту(z заменяется на a).
               "2.В заданной строке поменять порядок слов на обратный
               "(слова разделены пробелами)
               "3.Дана строка, слова которой разделены пробелами.Распознать в ней слова
               "в шестнадцатиричной системе счисления, и вывести их десятичный эквивалент*/
    class Program
    {
        static void Main(string[] args)
        {
           
            int switchMenu;
            bool locks;
            do
            {
                Console.WriteLine("Выберите задание:\n1.Дана строка, состоящая из строчных \n" +
               "английских букв. Заменить в ней все буквы,стоящие после гласных,\n" +
               " на следующие по алфавиту(z заменяется на a).\n" +
               "2.В заданной строке поменять порядок слов на обратный \n" +
               "(слова разделены пробелами).\n" +
               "3.Дана строка, слова которой разделены пробелами.Распознать в ней слова,\n" +
               "в шестнадцатиричной системе счисления, и вывести их десятичный эквивалент.");
                locks = false;
                int.TryParse(Console.ReadLine(), out switchMenu);
                Console.Clear();
                switch (switchMenu)
                {
                    case 1: Zad1(); break;
                    case 2: Zad2(); break;
                    case 3: Zad3(); break;
                    default: locks = true; break;
                }
            } while (locks);
        }

        static void Zad1()
        {
            StringBuilder str;
            bool Check;
            do
            {
                Check = false;
                Console.WriteLine("Введите строку");
                str = new StringBuilder(Console.ReadLine());
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (str.ToString().Contains(j.ToString())) Check = true;
                    }
                }
            } while (Check);
            char[] c = { 'a', 'y', 'o', 'u', 'e' };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0, start = 0; j > -1 && start < str.Length; start = j + 1)
                {
                    j = str.ToString().IndexOf(c[i], start);
                    if (j == str.Length - 1)
                        break;
                    if (j == -1)
                        break;
                    if (str[j + 1] == 'z')
                    {
                        str.Insert(j + 2, 'a');
                        str.Remove(j + 1, 1);
                        continue;
                    }
                    str.Insert(j + 2, (char)(str[j + 1] + 1));
                    str.Remove(j + 1, 1);
                }
            }
            Console.WriteLine(str);
        }

        static void Zad2()
        {
            Console.WriteLine("Введите слова через пробел");
            string str = Console.ReadLine();
            string[] split = str.ToString().Split(' ');
            for (int i = 0, l = split.Length; (i < split.Length) && (i < l); i++, l--)
            {
                string temp = split[i];
                split[i] = split[l - 1];
                split[l - 1] = temp;
            }
            str = String.Join(" ", split);
            Console.WriteLine(str);
        }
        static void Zad3()
        {
            Console.WriteLine("Введите слова через пробел");
            string str = Console.ReadLine();
            string[] split = str.ToString().Split(' ');
            foreach (string hex in split)
            {
                bool locks = true;
                for (int i = 0; i < hex.Length; i++)
                {
                    if ((hex[i] < '0' || hex[i] > '9') && (hex[i] < 'A' || hex[i] > 'F'))
                    {
                        locks = false;
                    }
                }
                if (locks && hex != "")
                {
                    int value = Convert.ToInt32(hex, 16);
                    Console.WriteLine($"hexadecimal value = {hex}, int value = {value} ");
                }

            }
        }
    }
}
;
