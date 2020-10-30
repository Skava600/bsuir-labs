using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace lab3
{
    class Infantry : Unit
    {
        protected const int MAXATTACK = 8;
        public override void Train()
        {
            Console.WriteLine($"{type} поупражнялся с манекенами");
            if (attackUnit < MAXATTACK)
                attackUnit++;
            else Console.WriteLine("Вы не можете бить сильнее");
        }
        public Infantry() : base(12, 3, 0.8, 6, "Пехотинец")
        {
            rangeUnit = (int)RangeUnits.Infantry;
            intType = Type.Infantry;
        }
        public Infantry(double a, double b, double c, double g, string d) : base(a, b, c, g, d)
        {
            
        }
    }
}
