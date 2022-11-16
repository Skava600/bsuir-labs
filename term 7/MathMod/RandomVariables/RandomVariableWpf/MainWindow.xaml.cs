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
        const int Intervals = 100;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            int b = 1;

            var vm = DataContext as RandomVariableViewModel;
            if (vm != null)
            {
                List<double> dots = new List<double>();
                foreach(var varibale in new Generator(100_000, a, b))
                {
                    dots.Add(varibale);
                }

                dots.Sort();

                int[] counts = new int[Intervals];
                double len = (b - a) / (double)Intervals;

                for (int i = 0, prev = 0; i < Intervals; i++)
                {
                    counts[i] = dots.Count(x => x <= a + ((i + 1) * len)) - prev;
                    prev += counts[i];
                }

                int j = 0;
                vm.HistData = new System.Collections.ObjectModel.ObservableCollection<TaskModel>(counts.Select(num => new TaskModel(j++, num)));
            }
        }
    }
}
