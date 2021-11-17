using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace lab3
{
    class HorseMan : Infantry, IHorse
    {

        float IHorse.Speed { get => SpeedUnit; set => SpeedUnit = value; }
        public HorseMan() : base(rnd.Next(10,30), rnd.Next(5,MAXATTACK), 0.6f, 0, "Cavalry") // hp,damage,armour,speed,type
        { 
            SpeedUnit = rnd.Next(8,IHorse.MAXSPEED); 
            RangeUnit = RangeUnit.HorseMan;
            IntType = Type.HorseMan;
        }
        public override void Train()
        {
            Console.WriteLine($"{TypeStr} galloped across the train field and ");
            base.Train();
            if (SpeedUnit < IHorse.MAXSPEED)
                SpeedUnit++;
            else Console.WriteLine("You can't make your hourse run faster.");
        }


    };

    //*************************************************************
}
