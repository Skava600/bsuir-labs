using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEvents
{
    public class ListEqualityComparer<T> : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> lhs, List<T> rhs)
        {
            return lhs.SequenceEqual(rhs);
        }

        public int GetHashCode(List<T> list)
        {
            unchecked
            {
                int hash = 23;
                foreach (T item in list)
                {
                    hash = (hash * 31) + (item == null ? 0 : item.GetHashCode());
                }
                return hash;
            }
        }
    }
}
