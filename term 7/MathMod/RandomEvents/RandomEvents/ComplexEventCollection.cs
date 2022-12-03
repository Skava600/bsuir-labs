using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    public class ComplexEventCollection: IEnumerable<List<bool>>
    {
        private readonly List<int> independentEvents;
        private readonly int n;
        public ComplexEventCollection(List<int> independentEvents, int n)
        {
            this.independentEvents = independentEvents;
            this.n = n;
        }



        public IEnumerator<List<bool>> GetEnumerator()
        {
            List<bool> bools;
            for (int i = 0; i < n; i++)
            {
                bools = new List<bool>();
                bools.AddRange(Generate());

                yield return bools;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private List<bool> Generate()
        {
            List<bool> result = new List<bool>();
            for (int i = 0; i < independentEvents.Count; i++)
            {
                result.Add(SimpleEventCollection.Generate(independentEvents[i]));
            }

            return result;
        }
    }
}
