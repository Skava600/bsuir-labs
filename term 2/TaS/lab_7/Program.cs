using System;
using System.Runtime;

namespace _7lab
{
    class Program
    {
        public static long InputLong()
        {
            Console.WriteLine("Введите n");
            long n;
            while (!long.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Ошибка ввода! Введите целое число n");
            }
            return n;
        }
        static void Main(string[] args)
        {
            Rational a,b;
            a = Rational.Parse("-3,25");
            b = Rational.Parse("1/3");
            bool finish = false;
            Rational c = new Rational();
            do
            {
                Console.Clear();
                Console.WriteLine("a = " + a + " = " + a.ToString("correct")+ " = " + a.ToString("incorrect")
                    + " = " + a.ToString("float"));
                Console.WriteLine("b = " + b + " = (float)" + b.ToString("float") + " = (decimal)" + 
                    b.ToString("decimal") +  " = (double)" + b.ToString("double") + " = (double)" +
                    (double)b + " = (long)" + b.ToString("long"));
                if (a > b)
                {
                    Console.WriteLine("a > b");
                }
                if (a < b)
                {
                    Console.WriteLine("a < b");
                }
                if (a == b)
                {
                    Console.WriteLine("a == b");
                }
                if (a != b)
                {
                    Console.WriteLine("a != b");
                }
                if (a <= b)
                {
                    Console.WriteLine("a <= b");
                }
                if (a >= b)
                {
                    Console.WriteLine("a >= b");
                }
                int choose;
                Console.WriteLine("\nВыберите действие.\n1.c = a + b\n2.c = a - b.\n" +
                    "3.c = a * b\n4.c = a/b.\n5.++a\n6.b+=n.\n7.a=*n.\n8.Выход к сортированию массива");
                
                while (!int.TryParse(Console.ReadLine(), out choose) || choose < 1 || choose > 8)
                {
                    Console.WriteLine("Ошибка ввода! Введите целое число choose до 9");
                }
                switch (choose)
                {
                    case 1: c = a + b; Console.WriteLine("c = " + a + " + " + b + " = " + c );break;
                    case 2: c = a - b; Console.WriteLine("c = " + a + " - " + b + " = " + c); break;
                    case 3: c = a * b; Console.WriteLine("c = " + a + " * " + b + " = " + c); break;
                    case 4: c = a / b; Console.WriteLine("c = " + a + " / " + b + " = " + c); break;
                    case 5: Console.WriteLine("++a = " + ++a); break;
                    case 6: long n = InputLong(); Console.WriteLine("b = " + b + " + " +  n  + " = " + (Rational)(b +=  + n)); break;                     
                    case 7: n = InputLong(); Console.WriteLine("a = " + a + " * " + n + " = " +  (Rational)(a= n * a));break;
                    case 8: finish = true;break;
                }

                Console.ReadKey();
            } while (!finish);
            Rational[] list = new Rational[10];
            Random rnd = new Random();
            Console.WriteLine("Неотсортированный массив");
            for(int i = 0; i < 10; i++)
            {
                try
                {
                    list[i] = new Rational(rnd.Next(-50, 50), rnd.Next(-50, 50));
                }
                catch
                {
                    list[i] = new Rational(rnd.Next(-50, 50));
                }
                Console.Write($"{list[i].ToString()} ");
            }
            Array.Sort(list);
            Console.WriteLine("\nОтсортированный массив");
            for(int i = 0; i < 10; i++)
            {
                Console.Write($"{list[i].ToString()} ");
            }


        }
    }
}
