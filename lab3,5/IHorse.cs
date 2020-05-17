using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace lab3
{
    interface IHorse
    {
        protected const int MAXSPEED = 20;
        double Speed { get; set; }
        public void CleanPotato()
        {
            Console.WriteLine(" Лошадь не может чистить картошку");
        }
        public void StandUp()
        {
            Console.WriteLine("Лошадь встала на дыбы");
        }

    };

}
