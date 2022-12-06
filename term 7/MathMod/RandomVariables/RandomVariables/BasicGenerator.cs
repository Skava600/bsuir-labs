using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariablesCore
{
    internal class BasicGenerator
    {
        const int RandMax = int.MaxValue;
        public static int Generate()
        {
            return new Random().Next(RandMax);
        }

    }
}
