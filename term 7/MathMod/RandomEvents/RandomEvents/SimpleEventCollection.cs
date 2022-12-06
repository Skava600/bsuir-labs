using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    public class SimpleEventCollection : IEnumerable<bool>
    {
        private readonly int probability;
        private readonly int n;

        public SimpleEventCollection(int probability, int n)
        {
            this.probability = probability;
            this.n = n;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < n; i++)
            {
                yield return Generate(probability);
            }
        }
        public static bool Generate(int probability)
        {
            int result = new Random().Next(101);
            if (result <= probability)
                return true;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();    
        }
    }
}
