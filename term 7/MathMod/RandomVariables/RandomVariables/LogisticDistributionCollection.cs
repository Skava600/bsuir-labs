using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    public class LogisticDistributionCollection
    {
        private readonly int _count;
        private double mu;
        private double s;
        public LogisticDistributionCollection(int count, double mu, double s)
        {
            _count = count;
          
            this.mu = mu;
            this.s = s;
        }

        public IEnumerator<double> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return GenerateLogisticRandomVariable();
            }
        }

        public double GenerateLogisticRandomVariable()
        {
            double result = mu + s * Math.Log(1.0 / UniformDistributionCollection.GenerateUniformRandomVariable(0, 1) - 1);
            return result;
        }

    }
}
