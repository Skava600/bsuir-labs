using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEventsWPF.Utils
{
    internal static class MyExtensions
    {
        public static string ListBoolToString(this List<bool> bools)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bools.Count; i++)
            {
                if (bools[i] == false)
                {
                    sb.Append("n");
                }
                sb.Append((char)(i + 65));
            }

            return sb.ToString();
        }
    }
}
