using Microsoft.Win32.TaskScheduler;
using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.WinForms.ListView;
using System.ComponentModel;
using TaskSchedulerProject.ExternalData;
using System.Security.Principal;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace TaskSchedulerProject
{
    public partial class MainForm : Form
    {
        Service service;
        List<ActionInfo> actionsDataSource;
        BindingList<TaskData> tasksDataSource;
        public MainForm()
        {
            InitializeComponent();
            service = new Service();

            RefreshGridView();
           
            CustomizeGridView();        
        }

        private List<Task> RefreshGridView()
        {
            List<TaskData> list = new List<TaskData>();
            foreach (var task in service.ts.AllTasks)
            {
                list.Add(new TaskData(task));
            }

            tasksDataSource = new BindingList<TaskData>(list);
            this.gridGroupingControl1.DataSource = tasksDataSource;
            return new List<Task>();
        }

        void CustomizeGridView()
        {           
            this.gridGroupingControl1.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.MultiExtended;
            this.gridGroupingControl1.Table.SelectedRecords.Add(this.gridGroupingControl1.Table.Records[0]);
        }

        private List<ActionInfo> GetActions()
        {
            List<ActionInfo> actionsInfoCollection = new List<ActionInfo>();
            actionsInfoCollection.Add(new ActionInfo() { ActionName = "Create task...", Category = "Task Scheduler", Image = @"..\..\..\icons\s-task-icon.png", Action = Service.CreateTask });
            actionsInfoCollection.Add(new ActionInfo() { ActionName = "Refresh", Category = "Task Scheduler", Image = @"..\..\..\icons\refresh-icon.png", Action = (x) => RefreshGridView() });
            if (gridGroupingControl1.Table.SelectedRecords.Count == 1)
            {
                actionsInfoCollection.Add(new ActionInfo() { ActionName = "Properties...", Category = "Choosed tasks", Image = @"..\..\..\icons\settings-icon.png", Action = Service.EditTask });
            }
            if (gridGroupingControl1.Table.SelectedRecords.Count != 0)
            {
                actionsInfoCollection.Add(new ActionInfo() { ActionName = "Delete", Category = "Choosed tasks", Image = @"..\..\..\icons\delete-icon.png", Action = service.DeleteTasks });
                actionsInfoCollection.Add(new ActionInfo() { ActionName = "Execute...", Category = "Choosed tasks", Image = @"..\..\..\icons\play-icon.png", Action = Service.ExecuteTasks });
                actionsInfoCollection.Add(new ActionInfo() { ActionName = "Disable...", Category = "Choosed tasks", Image = @"..\..\..\icons\down-arrow-icon.png", Action = Service.SwitchStateTasks });
            }

            foreach (var record in gridGroupingControl1.Table.SelectedRecords)
            {
                if (record.Record.GetValue(nameof(Task.State)).Equals(TaskState.Disabled.ToString()))
                {
                    actionsInfoCollection.Add(new ActionInfo() { ActionName = "Enable", Category = "Choosed tasks", Image = @"..\..\..\icons\up-arrow-icon.png", Action = Service.SwitchStateTasks });
                    actionsInfoCollection.Remove(actionsInfoCollection.FirstOrDefault(act => act.ActionName.Equals("Execute...")) ?? new ActionInfo());
                    actionsInfoCollection.Remove(actionsInfoCollection.FirstOrDefault(act => act.ActionName.Equals("Disable...")) ?? new ActionInfo());
                    break;
                }
            }
            return actionsInfoCollection;
        }
       

        private void sfListView1_SelectionChanged(object sender, Syncfusion.WinForms.ListView.Events.ItemSelectionChangedEventArgs e)
        {
            if (e.AddedItems.First().GetType().Name == nameof(GroupResult))
            {
                return;
            }
            var actionInfo = this.actionsDataSource.Find(x => x.Action == ((ActionInfo)e.AddedItems.First()).Action) ?? new ActionInfo() { Action = { } };
            var records = gridGroupingControl1.Table.SelectedRecords;
            List<Task> selectedTasks = new List<Task>();
            foreach (var record in records)
            {
                selectedTasks.Add(service.ts.AllTasks.First(t => t.Name.Equals(record.Record.GetValue(nameof(t.Name)))));
            }

            List<Task> tasks = new List<Task>();
            try
            {
                tasks = actionInfo.Action(selectedTasks);
            }
            catch (UnauthorizedAccessException)
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                MessageBox.Show($"Cannot delete task with your current identity '{identity.Name}' permissions level." +
                    "You likely need to run this application 'as administrator' even if you are using an administrator account.", "Attention");
            }

            if (actionInfo.Action == Service.CreateTask)
            {
                tasks.ForEach(t => tasksDataSource.Add(new TaskData(t)));

                gridGroupingControl1.DataSource = tasksDataSource;
            }
        
            if (actionInfo.Action == service.DeleteTasks)
            {
                tasks.ForEach(t => tasksDataSource.Remove(tasksDataSource.First(x => x.item.Name.Equals(t.Name))));

                gridGroupingControl1.DataSource = tasksDataSource;
            }

            if (actionInfo.Action == Service.EditTask || actionInfo.Action == Service.SwitchStateTasks)
            {
                RefreshGridView();
            }

            var listView = (SfListView) sender;
            listView.SelectedIndex = -1;
        }

        private void sfListView1_DrawItem(object sender, Syncfusion.WinForms.ListView.Events.DrawItemEventArgs e)
        {
            var action = actionsDataSource.Find(x => x.ActionName.Equals(e.Text)) ?? new ActionInfo();
            if (!String.IsNullOrEmpty(action.Image))
                e.Image = Image.FromFile(action.Image);
            e.ImageAlignment = ContentAlignment.BottomLeft;
        }


        private void ActionsSetChanged()
        {
            sfListView1.DataSource = actionsDataSource;
            sfListView1.DisplayMember = "ActionName";
            sfListView1.View.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Category",
            });
        }

        private void gridGroupingControl1_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            actionsDataSource = GetActions();
            ActionsSetChanged();
            LoadTaskInfo();
        }

        private void LoadTaskInfo()
        {
            if (gridGroupingControl1.Table.SelectedRecords.Count == 1)
            {
                tabControlAdv1.Visible = true;
                var task = tasksDataSource.FirstOrDefault(t => t.Name.Equals(gridGroupingControl1.Table.SelectedRecords[0].Record.GetValue(nameof(t.Name))));
                if (task == null)
                {
                    tabControlAdv1.Visible = false;
                    return;
                }

                textBoxExtName.Text = task.Name;
                textBoxExtLocation.Text = task.item.Folder.ToString();
                textBoxExtDescription.Text = task.item.Definition.RegistrationInfo.Description;
                textBoxExtUserAccount.Text = task.item.Definition.Principal.ToString();
                labelAuthor2.Text = task.Author;
                radioButtonAdvLoggedOn.Checked = task.item.Definition.Settings.RunOnlyIfLoggedOn;
                radioButtonAdv2.Checked = !task.item.Definition.Settings.RunOnlyIfLoggedOn;
                checkBoxAdvHidden.Checked = task.item.Definition.Settings.Hidden;
                checkBoxAdvRunHighestPriv.Checked = task.item.Definition.Principal.RunLevel.Equals(TaskRunLevel.Highest);
                textBoxExtCompatibility.Text = TaskCompatibilityExtensions.ToFriendlyString(task.item.Definition.Settings.Compatibility);
                
                List<TriggerData> triggers = new List<TriggerData>();
                List<ActionsData> actions = new List<ActionsData>();

                task.item.Definition.Triggers.ForEach(t => triggers.Add(new TriggerData(t)));
                task.item.Definition.Actions.ForEach(a => actions.Add(new ActionsData(a)));
                gridGroupingControl2.DataSource = triggers;
                gridGroupingControlActions.DataSource = actions;
                gridGroupingControl2.Visible = task.item.Definition.Triggers.Count > 0;

                checkBoxAdvRunOnDemand.Checked = task.item.Definition.Settings.AllowDemandStart;
                checkBoxAdvRunTaskAsSoon.Checked = task.item.Definition.Settings.StartWhenAvailable;
                checkBoxAdvForceToStop.Checked = task.item.Definition.Settings.AllowHardTerminate;
                textBoxExtRestartTimes.Text = (task.item.Definition.Settings.RestartCount == 0 ? 3  : task.item.Definition.Settings.RestartCount).ToString();
                
                comboBoxAdvRestartInterval.Text = task.item.Definition.Settings.RestartInterval.Ticks == 0 ? "1 minute" : task.item.Definition.Settings.RestartInterval.ToString();
                comboBoxAdvDeleteCondition.Text = task.item.Definition.Settings.DeleteExpiredTaskAfter.Ticks == 0 ? "30 days" : task.item.Definition.Settings.DeleteExpiredTaskAfter.ToString();
            }
            else
            {
                tabControlAdv1.Visible = false;
            }
        }

    }
}