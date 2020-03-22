using System;

namespace l1_matr
{
    class Program
    {
        static void Plus(int[,] a, int[,] b, int n, int m, int n1, int m1, char zn)
        {
            if (n != n1 || m != m1)
            {
                Console.Write("Матрицы несовместны");
                return;
            }
            int[,] c = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (zn == '+')
                        c[i, j] = a[i, j] + b[i, j];
                    else c[i, j] = a[i, j] - b[i, j];
                }
            for (int i = 0; i < n1; i++)
            {
                Console.Write("\n|\t");
                for (int j = 0; j < m1; j++)
                {

                    Console.Write($"{c[i, j]} \t");

                }
                Console.Write("|");
            }

        }
        static void Mult(int[,] a, int[,] b, int n, int m, int n1, int m1)
        {
            if (m != n1)
            {
                Console.Write("Матрицы не сопряжены");
                return;
            }
            int[,] c = new int[n, m1];
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m1; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < m; k++)
                        c[i, j] += a[i, k] * b[k, j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                Console.Write("\n|\t");
                for (int j = 0; j < m1; j++)
                {

                    Console.Write($"{c[i, j]} \t");

                }
                Console.Write("|");
            }

        }


        static void Vivod(int[,] a, int[,] b, int n, int m, int n1, int m1)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write("\n|\t");
                for (int j = 0; j < m; j++)
                    Console.Write($"{a[i, j]} \t");
                Console.Write("|");

            }
            for (int i = 0; i < n1; i++)
            {
                Console.Write("\n|\t");
                for (int j = 0; j < m1; j++)
                {
                    Console.Write($"{b[i, j]} \t");
                }
                Console.Write("|");
            }

        }
        static void Vod(ref int[,] a, ref int[,] b, ref int n, ref int m, ref int n1, ref int m1)
        {

            Console.WriteLine("\nВведите размерность матрицы А ");
            n = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());
            a = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"a[{i}][{j}]=");
                    a[i, j] = Convert.ToInt32(Console.ReadLine());
                }

            Console.WriteLine("Введите размерность матрицы Б");
            n1 = Convert.ToInt32(Console.ReadLine());
            m1 = Convert.ToInt32(Console.ReadLine());
            b = new int[n, m];
            for (int i = 0; i < n1; i++)
                for (int j = 0; j < m1; j++)
                {
                    Console.Write($"a[{i}][{j}]=");
                    b[i, j] = Convert.ToInt32(Console.ReadLine());
                }

        }

        static void Minor (int [,] a, int n, ref int [,] p, int bi, int bj)
        {
            int k = 0, l; // пропуск переменной и строки
            for(int i = 0;i < n - 1 ; i++)
            {
                if (i == bi) k = 1;
                l = 0;
                for(int j = 0;j < n - 1;j++)
                {
                    if (j == bj) l = 1;
                    p[i, j] = a[i + k, j + l];
                }
            }
        }

        static int Dim (int [,] a, int n, int m, int d)
        {
            int k = 1 ;
            if (n !=m)
            {
                Console.Write("Матрица не квадратная");
                return 0;
            }
            int[,] p = new int[n, n];
            if (m == 1)
            {
                d = a[0,0];
            }
            else if(m == 2)
            {
                d = a[0, 0] * a[n - 1, n - 1] - a[0, n - 1] * a[n - 1, 0];
            }
            else
            {
                m = n - 1;
                for (int i=0; i < n;i++)
                {
                    Minor(a, n, ref p, i, 0);
                    Vivod(p, a, m, m, n, n);
                    d += k * a[i, 0] * Dim(p, m, m, d);
                    k *= -1;
                }
            }
            return d;
        }
        static void Menu(ref int[,] a, ref int[,] b, ref int n, ref int m, ref int n1, ref int m1)
        {
            int d ;
            Console.WriteLine("\nВыберите действие \n1.Ввод матриц\n2.Вывод матрицы\n3.Сложение\n" +
                "4.Вычитание\n5.Умножение\n6.Определитель А\n7.Определитель B\n8.Выйти ");
            int x;
            x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1 : Vod (ref a, ref b, ref n, ref m, ref n1, ref m1); break;
                case 2 : Vivod (a, b, n, m, n1, m1); break;
                case 3 : Plus (a, b, n, m, n1, m1, '+'); break;
                case 4 : Plus (a, b, n, m, n1, m1, '-'); break;
                case 5 : Mult (a, b, n, m, n1, m1); break;
                case 6:
                    {
                        d = Dim(a, n, m,0);
                        Console.WriteLine($"Dimension={d}");
                    }
                    break;
                case 7:
                    {
                        d = Dim(b, n, m,0);
                        Console.WriteLine($"Dimension={d}");
                    }
                    break;
                case 8 : return;
                default: break;

            }
            Menu (ref a, ref b, ref n, ref m, ref n1, ref m1);
        }


        static void Main (string[] args)
        {
            int n, m, n1, m1;
            Console.WriteLine ("Введите размерность матрицы А");
            n = Convert.ToInt32 (Console.ReadLine());
            m = Convert.ToInt32 (Console.ReadLine());
            int[,] a = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Console.Write ($"\na[{i}][{j}]=");
                    a[i, j] = Convert.ToInt32 (Console.ReadLine());
                }
            for (int i = 0; i < n; i++)
            {
                Console.Write ("\n|\t");
                for (int j = 0; j < n; j++)
                    Console.Write ($"{a[i, j]} \t");
                Console.Write ("|");
            }
            Console.WriteLine ("\nВведите размерность матрицы Б");
            n1 = Convert.ToInt32 (Console.ReadLine());
            m1 = Convert.ToInt32 (Console.ReadLine());
            int[,] b = new int[n, m];
            for (int i = 0; i < n1; i++)
                for (int j = 0; j < m1; j++)
                {
                    Console.Write ($"\nb[{i}][{j}]=");
                    b[i, j] = Convert.ToInt32 (Console.ReadLine());
                }
            for (int i = 0; i < n1; i++)
            {
                Console.Write ("\n|\t");
                for (int j = 0; j < m1; j++)
                {
                    Console.Write($"{b[i, j]} \t");
                }
                Console.Write("|");

            }
            Menu(ref a, ref b, ref n, ref m, ref n1, ref m1);
        }

    }
}
