using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariableWpf.Models
{
    internal class TaskModel
    {
        public int Num { get; set; }
        public int Count { get; set; }
        public TaskModel(int num,  int count)
        {
            Count = count;
            Num = num;
        }
    }
}
