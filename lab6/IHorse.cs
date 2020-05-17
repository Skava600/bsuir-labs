using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace lab3
{
    interface IHorse
    {
        protected const int MAXSPEED = 20;
        float Speed { get; set; }
        void StandUp()
        {
            Console.WriteLine("Horse Standed up.");
        }

    };

}
