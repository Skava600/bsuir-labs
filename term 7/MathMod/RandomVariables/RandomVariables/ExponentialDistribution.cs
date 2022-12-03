using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    internal class ExponentialDistribution
    {
        static double[] stairWidth = new double[257], 
            stairHeight = new double[256];
        const double x1 = 7.69711747013104972;
        const double A = 3.9496598225815571993e-3; /// area under rectangle

        public void setupExpTables()
        {
            // coordinates of the implicit rectangle in base layer
            stairHeight[0] = Math.Exp(-x1);
            stairWidth[0] = A / stairHeight[0];
            // implicit value for the top layer
            stairWidth[256] = 0;
            for (uint i = 1; i <= 255; ++i)
            {
                // such x_i that f(x_i) = y_{i-1}
                stairWidth[i] = -Math.Log(stairHeight[i - 1]);
                stairHeight[i] = stairHeight[i - 1] + A / stairWidth[i];
            }
        }

        double ExpZiggurat()
        {
            int iter = 0;
            do
            {
                int stairId = BasicGenerator.Generate() & 255;
                double x = UniformDistributionCollection.GenerateUniformRandomVariable(0, stairWidth[stairId]); // get horizontal coordinate
                if (x < stairWidth[stairId + 1]) /// if we are under the upper stair - accept
                    return x;
                if (stairId == 0) // if we catch the tail
                    return x1 + ExpZiggurat();
                if (UniformDistributionCollection.GenerateUniformRandomVariable(stairHeight[stairId - 1], stairHeight[stairId]) < Math.Exp(-x)) // if we are under the curve - accept
                    return x;
                // rejection - go back
            } while (++iter <= 1e9); // one billion should be enough to be sure there is a bug
            return double.NaN; // fail due to some error
        }

        public double Exponential(double rate)
        {
            return ExpZiggurat() / rate;
        }
    }
}
