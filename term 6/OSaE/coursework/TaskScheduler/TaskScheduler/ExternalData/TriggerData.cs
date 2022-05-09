using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerProject.ExternalData
{
    internal class TriggerData
    {
        public Trigger trigger;
        public TriggerData(Trigger trigger)
        {
            this.trigger = trigger;
        }

        public string Name { get => trigger.TriggerType.ToString(); }

        public string Details { get => trigger.ToString() ;}

        public string Status { get => trigger.Enabled ? "Enabled": "Disabled" ; }  
    }
}
