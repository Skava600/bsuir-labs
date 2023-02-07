using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystem
{
    public class Canteen
    {
        public double X { get; set; }
        // parameter of the exponential distribution
        public double p { get; set; }
        // share of visitors taking both the first and second courses
        public double q { get; set; }
        // time for a visitor to eat one dish
        public double t { get; set; }

        public double ro { get =>( X * ( q + 1))/ p; }


        public bool isStationary => X < p / (q + 1);


        //полное ожидание случайно величины Т
        public double MT => (2 / p) * q + (1 - q) * 1 / p;

        // матожидание при первой гипотезе по показательному закону D(T/H1) + (M(T/H1))^2
        public double MT2H1 => 1 / Math.Pow(p, 2) + Math.Pow(1 / p, 2);
        public double MT2H2 => 2 / Math.Pow(p, 2) + Math.Pow(2 / p, 2);
        // второй начальный момент
        public double MT2 => (1-q) * MT2H1 + q * MT2H2;

        public double DT => MT2 - MT * MT;
        // коэф вариации
        public double v => DT / MT;

        // формула полячека-хинчина
        public double GetAverageQueueLength() => Math.Pow(ro, 2) * Math.Pow((1 + v), 2) / 2 * (1 - ro);


        public double GetAverageStayTime()
        {
            return (GetAverageQueueLength() / X) + (q + 1) / p + (q + 1) * t;
        }
    }
}
