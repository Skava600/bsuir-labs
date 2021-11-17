using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    class HorseArcher : Archer, IHorse
    {
        double IHorse.Speed { get => speedUnit; set => speedUnit = value; }
        public override void Train()
        {
            Console.WriteLine($"{type} Проскакал по тренировочному полю и ");
            if(speedUnit < IHorse.MAXSPEED)
                 speedUnit++;
            base.Train();
        }
        public HorseArcher() : base(15, 4, 0.6, 0, "Конный Лучник")
        {
            speedUnit = 10;
            rangeUnit = (int)RangeUnits.HorseArcher;
            intType = Type.HorseArcher;
        }
    }
}

