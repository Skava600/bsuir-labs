using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace TaskSchedulerProject.ExternalData
{
    internal class ActionInfo
    {
        public string ActionName { get; set; }
        public string Category { get; set; }
        public string Image { get; set; } = "";
        public Func<List<Task>, List<Task>> Action { get; set; }
    }
}
