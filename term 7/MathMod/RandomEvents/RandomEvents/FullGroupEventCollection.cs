using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    public class FullGroupEventCollection: IEnumerable<int>
    {
        private List<int> thresholds = new List<int>();
        private readonly int n;
        public FullGroupEventCollection(List<int> events, int n)
        {
            int sum = 0;
            for (int i = 0; i < events.Count; i++)
            {
                thresholds.Add(sum);
                sum += events[i];
            }
            thresholds.Reverse();

            this.n = n;
        }

        private int Generate()
        {
            int p = new Random().Next(101);

            for (int i = 0; i < thresholds.Count; i++)
            {
                if (p >= thresholds[i])
                    return i + 1;
            }

            return 0;

        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < n; i++)
            {
                yield return Generate();
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
