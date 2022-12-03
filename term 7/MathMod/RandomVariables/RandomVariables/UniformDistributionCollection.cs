using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    public class UniformDistributionCollection
    {
        private readonly int _count;
        private double a;
        private double b;
        private static readonly Random  random = new Random();
        public UniformDistributionCollection(int count, double a, double b)
        {
            _count = count;
            if (a > b)
            {
                throw new ArgumentException(nameof(a));
            }
            this.a = a;
            this.b = b;
        }

        public IEnumerator<double> GetEnumerator()
        {
           for (int i = 0; i < _count; i++)
            {
                yield return GenerateUniformRandomVariable(a, b);
            }
        }

        public static double GenerateUniformRandomVariable(double a, double b)
        {
            return a + random.NextDouble() * (b - a);
        }


    }
}
