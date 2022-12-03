using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    public class NegativeBinomialDistributionCollection
    {
        private readonly int _count;
        private double p;
        private int r;
        public NegativeBinomialDistributionCollection(int count, double p, int r)
        {
            _count = count;
            this.p = p;
            this.r = r;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return GenerateNegativeBinimialDistributedRandomVariable(p, r);
            }
        }

        public static int GenerateNegativeBinimialDistributedRandomVariable(double p, int r)
        {
            return new NegativeBinomialDistribution(r, p).Generate();
        }

    }
}
