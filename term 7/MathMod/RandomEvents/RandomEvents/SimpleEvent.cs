using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    public class SimpleEvent
    {
        
        private int probability;
        public int Probability { get { return probability; } set {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Probability must be from zero to one", value.ToString());
                }

                probability = value; }
        }

        public SimpleEvent(int probability)
        {
            Probability = probability;
        }

        public bool Generate()
        {
            int result = new Random().Next(101);
            if (result <= probability)
                return true;
            return false;
        }
    }
}
