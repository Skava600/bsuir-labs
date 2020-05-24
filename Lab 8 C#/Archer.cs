using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    class Archer : Unit
    {
        protected const int MAXATTACK = 6;
        public override void Train()
        {
            Console.WriteLine($"{TypeStr} shooted at target");
            if (AttackUnit < MAXATTACK)
                AttackUnit++;
            else Console.WriteLine("You can't shoot stronger");
        }
        public Archer() : base(rnd.Next(7,12), rnd.Next(4,8), (float)0.8, rnd.Next(4,10), "Archer") // hp,damage,armour,speed,type
        {
            RangeUnit = RangeUnit.Infantry;
            IntType = Type.Archer;
        }
        public Archer(float a, float b, float c, float g, string d) : base(a, b, c, g, d)
        {
        }
    }
}
