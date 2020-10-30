using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    class Archer : Unit
    {
        protected const int MAXATTACK = 9;
        public override void Train()
        {
            Console.WriteLine($"{type} пострелял в мишень");
            if (attackUnit < MAXATTACK)
                attackUnit++;
            else Console.WriteLine("Вы не можете стрелять мощнее");
        }
        public Archer() : base(12, 4, 0.8, 6, "Лучник")
        {
            rangeUnit = (int)RangeUnits.Infantry;
            intType = Type.Archer;
        }
        public Archer(double a, double b, double c, double g, string d) : base(a, b, c, g, d)
        {

        }
    }
}
