using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace TaskSchedulerProject.ExternalData
{
    internal class TaskData
    {
        public Task item;
        public TaskData(Task item)
        {
            this.item = item;
        }

        public string Name { get => item.Name; }

        public string State => item.State == null ? "" : item.State.ToString();

        public string Triggers { get => item.Definition.Triggers.ToString(); }

        public string NextRunTime { get => item.NextRunTime.Equals(DateTime.MinValue) ? "" : item.NextRunTime.ToString(); }

        public string LastRunTime { get => item.LastRunTime.Equals(DateTime.MinValue) ? "" : item.LastRunTime.ToString(); }

        public string LastTaskResult { get => item.LastTaskResult.ToString(); }

        public string DateRegistration { get => item.Definition.RegistrationInfo.Date == DateTime.MinValue ? "" : item.Definition.RegistrationInfo.Date.ToString(); }

        public string Author { get => item.Definition.RegistrationInfo.Author; }
    }

}
