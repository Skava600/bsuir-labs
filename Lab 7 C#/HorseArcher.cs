using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    class HorseArcher : Archer, IHorse
    {
        float IHorse.Speed { get => SpeedUnit; set => SpeedUnit = value; }
        public override void Train()
        {
            Console.WriteLine($"{TypeStr} galloped across the train field and  ");
            if(SpeedUnit < IHorse.MAXSPEED)
                 SpeedUnit++;
            base.Train();
        }
        public HorseArcher() : base(rnd.Next(10,25), rnd.Next(3,MAXATTACK), 0.6f, 0, "Horse Archer") // hp,damage,armour,speed,type
        {
            SpeedUnit = rnd.Next(8, IHorse.MAXSPEED);
            RangeUnit = RangeUnit.HorseArcher;
            IntType = Type.HorseArcher;
        }
    }
}

