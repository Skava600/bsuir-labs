using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RandomVariableWpf.ViewModels;
using RandomVariablesCore;
using RandomVariableWpf.Models;
namespace RandomVariableWpf
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double a, b, confidenceLevel;
            if (double.TryParse(ATextBox.Text, out a) &&
                double.TryParse(BTextBox.Text, out b) &&
                double.TryParse(ConfidenceLevelBox.Text, out confidenceLevel))
            {

                var vm = DataContext as RandomVariableViewModel;
                if (vm != null)
                {
                    double[] dots = new double[N];
                    var generator = new UniformDistributionCollection(N, a, b);
                    int i = 0;
                    foreach (var varibale in generator)
                    {
                        dots[i++] = varibale;
                    }

                    chartControl.BeginInit();
                    vm.UniformData = new System.Collections.ObjectModel.ObservableCollection<double>(dots);
                    chartControl.EndInit();

                    StatisticsListBox.Text = StatisticsInfo.vCheckUniform(dots, confidenceLevel);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double p; int r; double confidenceLevel;
            if (double.TryParse(PTextBox.Text, out p) &&
                int.TryParse(RTextBox.Text, out r) &&
                double.TryParse(ConfidenceLevelBox.Text, out confidenceLevel))
            {

                var vm = DataContext as RandomVariableViewModel;
                if (vm != null)
                {
                    int[] dots = new int[N];
                    var generator = new NegativeBinomialDistributionCollection(N, p, r);
                    int i = 0;
                    foreach (var varibale in generator) 
                    {
                        dots[i++] = varibale;
                    }

                    chartControl.BeginInit();
                    vm.NegBinData = new System.Collections.ObjectModel.ObservableCollection<TaskModel>(
                        dots.GroupBy(num => num).Select(group => new TaskModel(group.Key, group.Count())));
                    chartControl.EndInit();

                    //StatisticsList2Box.Text = StatisticsInfo.vCheckUniform(dots, confidenceLevel);
                }
            }
        }
    }
}
