using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    internal class Lab1
    {
        public static Matrix Show(Matrix A, int[] x, int index)
        {
            #region Step 1
            //Matrix invertedA = A.CreateInvertibleMatrix();
            double[] l = new double[A.N];

            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.M; j++)
                {
                    l[i] += A[i, j] * x[j];
                }
            }

            if (l[index] == 0)
            {
                throw new ArgumentException("A- необратима");
            }

            Console.WriteLine($"\nStep 1: vector l");
            for (int i = 0; i < l.Length; i++)
            {
                Console.Write(l[i] + "\t");
            }
            #endregion

            #region Step 2
            double[] l1 = new double[l.Length];
            l.CopyTo(l1, 0);
            l[index] = -1;
            #endregion

            #region Step 3
            double[] l2 = new double[l.Length];
            
            for (int i = 0; i < l.Length; i++)
            {
                l2[i] = (-1.0 / l[index]) * l1[i];
            }

            Console.WriteLine($"\n\nStep 3: Column l2");

            for (int i = 0; i < l2.Length; i++)
            {
                Console.Write(l2[i] + "\t");
            }
            #endregion

            #region Step 4
            Matrix Q = new Matrix(l.Length);
            
            for (int i = 0; i < Q.N; i++)
            {
                Q[i, i] = 1;
            }

            for (int i = 0; i < Q.N; i++)
            {
                Q[i, index] = l2[i];
            }

            Console.WriteLine("\n\nStep 4: ");
            Q.Print();
            #endregion

            #region Step 5
            Matrix A1 = new Matrix(A.N);
            for (int i = 0; i < Q.N; i++)
            {
                for (int j = 0; j < Q.N; j++)
                {
                    A1[i, j] = j != index ? A[i, j] : 0;
                }
            }

            for (int i = 0; i < Q.N; i++)
            {
                for (int j = 0; j < Q.N; j++)
                {
                    A1[i, index] += A[i, j] * Q[j, index];
                }
            }

            Console.WriteLine("\nStep 5: ");
            A1.Print();
            #endregion
            return A1;
 
        }

    }
}
