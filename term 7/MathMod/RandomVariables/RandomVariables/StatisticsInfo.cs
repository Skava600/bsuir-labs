using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    public static class StatisticsInfo
    {
        static Dictionary<double, double> StudentDistributionTable = new Dictionary<double, double>() {
            { 0.95, 1.960 },
            { 0.99, 2.576 },
            { 0.999, 3.291} };

        public static double CountExpectedValue(double[] s)
        {
            double ans = 0d;
            foreach (var i in s)
                ans += i;
            return ans / s.Length;
        }

        public static double CountVariance(double[] s, double M)
        {
            double ans = 0d;
            foreach (var i in s)
                ans += Math.Pow((i - M), 2);
            return (ans / s.Length) * (s.Length / (s.Length - 1));
        }


        public static string vCheckUniform(double[] s, double confidenceLevel)
        {
            double E = CountExpectedValue(s);
            double Var = CountVariance(s, E);
            double estimationAccuracy = (Math.Sqrt(Var) * StudentDistributionTable[confidenceLevel]) / Math.Sqrt(s.Length);


            return $"E - {E}, Var - {Var} \n" +
                $"Confidence interval for expected value with confidence level {confidenceLevel} - ( {E - estimationAccuracy}, {E + estimationAccuracy} )\n";
        }
    }
}
