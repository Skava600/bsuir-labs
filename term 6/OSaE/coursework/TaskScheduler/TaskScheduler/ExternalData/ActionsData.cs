using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;
using Action = Microsoft.Win32.TaskScheduler.Action;

namespace TaskSchedulerProject.ExternalData
{
    internal class ActionsData
    {
        private Action action;

        public ActionsData(Action action)
        {
            this.action = action;
        }

        public string ActionType { get => action.ActionType.ToString(); }
        public string Detatils { get => action.ToString(); }

    }
}
