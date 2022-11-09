using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RandomEvents;
using RandomEventsWPF.ViewModels;

namespace RandomEventsWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int N = 1_000_000;
        public MainWindow()
        {
            InitializeComponent();
            FirstTask();
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskOneTab.IsSelected)
            {
                this.DataContext = new TaskOneViewModel();
                FirstTask();
            }
            else if(TaskTwoTab.IsSelected)
            {

            }
        }

        public void FirstTask()
        {
            //task 1
            int probability = 60;
            SimpleEvent simpleEvent = new SimpleEvent(probability);
            int trueCount = 0;
            for (int i = 0; i < N; i++)
            {
                if (simpleEvent.Generate())
                {
                    trueCount++;
                }
            }

            int falseCount = N - trueCount;

            TaskOneViewModel? vm = this.DataContext as TaskOneViewModel;
            if (vm != null)
            {
                vm.Models.Clear();
                vm.Models.Add(new TaskOneModel() { Count = trueCount, Name="True", Probability=(double)trueCount/N });
                vm.Models.Add(new TaskOneModel()
                {
                    Name ="True theoretical",
                    Probability = (double)probability / 100,

                });
                    vm.Models.Add(new TaskOneModel() { Count = falseCount, Name = "False", Probability = (double)falseCount / N });
                vm.Models.Add(new TaskOneModel()
                {
                    Name = "False theoretical",
                    Probability = 1 - ((double)probability / 100),

                });

            }
        }

        private void ButtonTaskOne_Click(object sender, RoutedEventArgs e)
        {
            FirstTask();
        }
    }
}
