using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace lab3
{
    class HorseMan : Infantry, IHorse
    {
       
        double IHorse.Speed { get => speedUnit; set => speedUnit = value; }
        public HorseMan() : base(15, 4, 0.6, 0, "Кавалерия") 
        { 
            speedUnit = 10; 
            rangeUnit = (int)RangeUnits.HorseMan;
            intType = Type.HorseMan;
        }
        public override void Train()
        {
            Console.WriteLine($"{type} проскакал по тренировочному полю и ");
            base.Train();
            if (speedUnit < IHorse.MAXSPEED)
                speedUnit++;
            else Console.WriteLine("Вы не можете выжать из лошади большую скорость.");
        }


    };

    //*************************************************************
}
