using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    public class ComplexDependentEventCollection : IEnumerable<List<bool>>
    {
        private readonly ushort P_A;
        private readonly ushort P_BA;
        private readonly int n;
        public ComplexDependentEventCollection(int n, ushort p_a, ushort p_ba)
        {
            P_A = p_a;
            P_BA = p_ba;
            this.n = n;
        }

        private List<bool> Generate()
        {
            bool a = SimpleEventCollection.Generate(P_A);
            bool b;
            if (a)
            {
                b = SimpleEventCollection.Generate(P_BA);
            }    
            else
            {
                b = SimpleEventCollection.Generate(100 - P_BA);
            }
            return new List<bool> { a, b };
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
    }
}
