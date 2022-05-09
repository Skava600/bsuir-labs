using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace TaskSchedulerProject
{
    internal class Service
    {
        public TaskService ts = TaskService.Instance;

        public static void EnumFolderTasks(TaskFolder fld, System.Action<Task> action)
        {
            foreach (Task task in fld.Tasks)
                action(task);
            foreach (TaskFolder sfld in fld.SubFolders)
                EnumFolderTasks(sfld, action);
        }

        public static List<Task> ExecuteTasks(List<Task> tasks)
        {
            foreach (Task task in tasks)
            {
                task.Run();
            }

            return tasks;
        }

        public static List<Task> SwitchStateTasks(List<Task> tasks)
        {
            foreach (Task task in tasks)
            {
                task.Enabled = !task.Enabled;
            }

            return tasks;
        }

        public List<Task> DeleteTasks(List<Task> tasks)
        {
            foreach (var t in tasks)
            {
                ts.RootFolder.DeleteTask(t.Name);
            }

            return tasks;
        }
        

        public static List<Task> CreateTask(List<Task> tasks)
        {
            const string taskName = "New Task";
            Task t = TaskService.Instance.AddTask(taskName,
              new TimeTrigger()
              {
                  StartBoundary = DateTime.Now + TimeSpan.FromHours(1),
                  Enabled = false
              },
              new ShowMessageAction("Hello world", "Title"));

            // Edit task and re-register if user clicks Ok
            TaskEditDialog editorForm = new TaskEditDialog(t);
            // ** The four lines above can be replaced by using the full constructor
            // TaskEditDialog editorForm = new TaskEditDialog(t, true, true);
            editorForm.ShowDialog();

            return new List<Task>() { t };
        }

        public static List<Task> EditTask(List<Task> tasks)
        {
            if (tasks.Count > 0)
            {
                TaskEditDialog editorForm = new TaskEditDialog(tasks.First());
                editorForm.ShowDialog();
            }
            return tasks;
        }
    }
}
