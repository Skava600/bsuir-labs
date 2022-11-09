using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    internal class ComplexEvent
    {
        private List<int> independentEvents;

        public ComplexEvent(List<int> independentEvents)
        {
            this.independentEvents = independentEvents;
        }

        public List<bool> Generate()
        {
            List<bool> result = new List<bool>();
            for (int i = 0; i < independentEvents.Count; i++)
            {
                int iResult = new Random().Next(101);
                if (iResult <= independentEvents[i])
                    result.Add(true);
                else result.Add(false);
            }

            return result;
        }
    }
}
