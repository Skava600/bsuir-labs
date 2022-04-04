using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(3);
            A.Input();

            Console.WriteLine("\nMatrix A:");
            A.Print();

            int[] x = new int[] { 1, 3, 2 };

            Lab1.Show(A, x, 1);
            Console.Read();
        }
    }
}
