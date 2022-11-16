using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariableWpf.Models
{
    internal class TaskModel
    {
        public int Index { get; set; }
        public int Hits { get; set; }
        public TaskModel(int index, int count)
        {
            Index = index;
            Hits = count;
        }
    }
}
