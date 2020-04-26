using System;
using System.Runtime.InteropServices;

namespace l4_2
{
    class Program
    {       
        [DllImport(@"G:\ucheba\2semeset\csharp\l4-2\Project1.dll")]
        public static extern double Add(double a, double b);
        [DllImport(@"G:\ucheba\2semeset\csharp\l4-2\Project1.dll")]
        public static extern double Sub(double a, double b);
        [DllImport(@"G:\ucheba\2semeset\csharp\l4-2\Project1.dll")]
        public static extern double Multiply(double a, double b);
        [DllImport(@"G:\ucheba\2semeset\csharp\l4-2\Project1.dll")]
        public static extern double Divide(double a, double b);
       
        [DllImport(@"G:\ucheba\2semeset\csharp\l4-2\Project1.dll")]
        public static extern double Pow(double a, double b);
        //*****ВВОД И ПРОВЕРКА НА ВВОД**********************************************
        public static double Input()
        {
            double x;
            while (!double.TryParse(Console.ReadLine(), out x))
            {
                Console.Write("Ошибка ввода! Введите целое число");
            }
            return x;
        }
       //*******************************************************************************

        static void Main(string[] args)
        {
            double a, b;         
            double resCalc;
            int chooseAct;
            do
            {
                Console.Write("Введите первое число");
                a = Input();
                Console.Write("Введите второе число");
                b = Input();
                Console.Write("Введите Действие :\n1.Сложение\n2.Вычитание\n3.Умножение\n4.Деление\n5.Возведение в степень\n6.Выйти ");
                chooseAct = Convert.ToInt32(Input());
                switch (chooseAct)
                {
                    case 1: resCalc = Add(a, b); break;
                    case 2: resCalc = Sub(a, b); break;
                    case 3: resCalc = Multiply(a, b); break;
                    case 4: resCalc = Divide(a, b); break;
                    case 5: resCalc = Pow(a, b); break;
                    default: break;
                }
                if(chooseAct > 0 && chooseAct < 6) Console.WriteLine($"Результат - {resCalc}");
            } while (chooseAct != 6);
        }
    }
}
