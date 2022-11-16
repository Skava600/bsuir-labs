using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    public class Generator
    {
        private readonly int _count;
        private int a;
        private int b;
        private readonly Random  random= new Random();
        public Generator(int count, int a, int b)
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
                yield return GenerateRandomVariable();
            }
        }

        private double GenerateRandomVariable()
        {
            return a + random.NextDouble() * (b - a);
        }
    }
}
