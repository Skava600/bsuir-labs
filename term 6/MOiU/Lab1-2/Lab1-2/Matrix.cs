using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    internal class Matrix
    {
        private readonly double[,] elements;

        private double precalculatedDeterminant = double.NaN;
        public int N 
        {
            get => elements.GetLength(0); }
        public int M
        {
            get => elements.GetLength(1);
        }

        public Matrix(int size)
        {
            elements = new double[size, size]; 
        }

        public Matrix (int n, int m)
        {
            elements = new double[n, m];
        
        }

        public void Input()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write($"Matrix[{i + 1}, {j + 1}]: ");
                    if (!double.TryParse(Console.ReadLine(), out elements[i, j]))
                    {
                        throw new ArgumentException("Wrong input.");
                    }
                }
            }
        }

        public Matrix CreateInvertibleMatrix()
        {
            if (this.M != this.N)
                return null;
            var determinant = CalculateDeterminant();
            if (Math.Abs(determinant) < 0.0000001)
                return null;

            var result = new Matrix(M, M);
            ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = ((i + j) % 2 == 1 ? -1 : 1) *
                    CalculateMinor(i, j) / determinant;
            });

            result = result.CreateTransposeMatrix();
            return result;
        }

        private double CalculateMinor(int i, int j)
        {
            return CreateMatrixWithoutColumn(j)
                .CreateMatrixWithoutRow(i)
                .CalculateDeterminant();
        }

        public double CalculateDeterminant()
        {
            if (!double.IsNaN(this.precalculatedDeterminant))
            {
                return this.precalculatedDeterminant;
            }
            if (this.M != this.N)
            {
                throw new InvalidOperationException("determinant can be calculated only for square matrix");
            }
            if (this.N == 2)
            {
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            }
            double result = 0;
            for (var j = 0; j < this.N; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * this[1, j] *
                    this.CreateMatrixWithoutColumn(j).CreateMatrixWithoutRow(1).CalculateDeterminant();
            }
            this.precalculatedDeterminant = result;
            return result;
        }

        public Matrix CreateTransposeMatrix()
        {
            var result = new Matrix(this.N, this.M);
            result.ProcessFunctionOverData((i, j) => result[i, j] = this[j, i]);
            return result;
        }

        private Matrix CreateMatrixWithoutRow(int row)
        {
            if (row < 0 || row >= this.M)
            {
                throw new ArgumentException("invalid row index");
            }

            var result = new Matrix(this.M - 1, this.N);
            result.ProcessFunctionOverData((i, j) => result[i, j] = i < row ? this[i, j] : this[i + 1, j]);
            return result;
        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            for (var i = 0; i < this.M; i++)
            {
                for (var j = 0; j < this.N; j++)
                {
                    func(i, j);
                }
            }
        }

        private Matrix CreateMatrixWithoutColumn(int column)
        {
            if (column < 0 || column >= this.N)
            {
                throw new ArgumentException("invalid column index");
            }
            var result = new Matrix(this.M, this.N - 1);
            result.ProcessFunctionOverData((i, j) => result[i, j] = j < column ? this[i, j] : this[i, j + 1]);
            return result;
        }


        public void Print()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write($"{Math.Round(elements[i, j], 3)}\t");
                }
                Console.WriteLine();
            }
        }

        public double this[int i, int j]
        {
            get => elements[i, j];
            set => elements[i, j] = value;
        }

    }
}
