using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace lab3
{
    class Infantry : Unit
    {
        
        protected const int MAXATTACK = 15;
        public override void Train()
        {
            Console.WriteLine($"{TypeStr} trained with dummy");
            if (AttackUnit < MAXATTACK)
                AttackUnit++;
            else Console.WriteLine("You can't be stronger");
        }
        public Infantry() : base(rnd.Next(10, 25), rnd.Next(3, MAXATTACK), (float)rnd.NextDouble(), rnd.Next(10, 15), "Infantry") // hp,damage,armour,speed,type
        {
            RangeUnit = RangeUnit.Infantry;
            IntType = Type.Infantry;
        }
        public Infantry(float a, float b, float c, float g, string d) : base(a, b, c, g, d)
        {
            
        }
    }
}
