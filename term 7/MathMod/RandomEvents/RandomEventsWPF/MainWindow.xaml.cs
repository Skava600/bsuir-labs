using RandomEvents;
using RandomEventsWPF.Utils;
using RandomEventsWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System;
using DevExpress.Xpf.Charts;

namespace RandomEventsWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int N = 1_000_000;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private int _wheelSpeed = 20;
        private long _wheelTime = 125;
        private long _wheelCurrentTime = 0;
        public MainWindow()
        {
            InitializeComponent();
            FirstTask();
        }

        private void FirstTask()
        {
            //task 1
            int probability = 50;
            int trueCount = 0;
            SimpleEventCollection simpleGenerator = new SimpleEventCollection(probability, N);
            foreach(var value in simpleGenerator)
            {
                if (value == true)
                    trueCount++;
            }

            int falseCount = N - trueCount;

            TaskViewModel? vm = this.DataContext as TaskViewModel;
            if (vm != null)
            {
                vm.Models.Clear();
                vm.Models.Add(new TaskModel() { Count = trueCount, Name="True", Probability=(double)trueCount/N });
                vm.Models.Add(new TaskModel()
                {
                    Name ="True theoretical",
                    Probability = (double)probability / 100,

                });
                    vm.Models.Add(new TaskModel() { Count = falseCount, Name = "False", Probability = (double)falseCount / N });
                vm.Models.Add(new TaskModel()
                {
                    Name = "False theoretical",
                    Probability = 1 - ((double)probability / 100),

                });

            }
        }

        private void SecondTask()
        {
            TaskViewModel? vm = this.DataContext as TaskViewModel;
            vm.Models.Clear();

            ComplexEventCollection complexEvent = new ComplexEventCollection(new List<int> { 60, 20, 30 }, N);

            List<List<bool>> results = new List<List<bool>>();
            foreach(var bools in complexEvent)
                results.Add(bools);
            
            var groupedResult = results.GroupBy(r => r, (key, lists) => new { List = key, Count = lists.Count() },
                           new ListEqualityComparer<bool>());

            foreach(var group in groupedResult)
            {
                vm.Models.Add(new TaskModel { Count = group.Count, Probability = (double)group.Count / N, Name = group.List.ListBoolToString() });
            }
        }

        private void ThirdTask()
        {
            TaskViewModel? vm = this.DataContext as TaskViewModel;
            vm.Models.Clear();

            ComplexDependentEventCollection dependentEvent = new ComplexDependentEventCollection(N, 50, 70);

            List<List<bool>> results = new List<List<bool>>();

            foreach (var bools in dependentEvent)
                results.Add(bools);

            var groupedResult = results.GroupBy(r => r, (key, lists) => new { List = key, Count = lists.Count() },
                           new ListEqualityComparer<bool>());

            foreach (var group in groupedResult)
            {
                vm.Models.Add(new TaskModel { Count = group.Count, Probability = (double)group.Count / N, Name = group.List.ListBoolToString() });
            }

        }

        private void FourthTask()
        {
            TaskViewModel? vm = this.DataContext as TaskViewModel;
            vm.Models.Clear();

            FullGroupEventCollection fullGroupEvent = new FullGroupEventCollection(new List<int> { 10, 50, 40 }, N);

            List<int> results = new List<int>();
            foreach(var value in fullGroupEvent)
                results.Add(value);

            var groupedResult = results.GroupBy(r => r, (key, numbers) => new {Key = key, Count = numbers.Count()});
            foreach (var group in groupedResult)
            {
                vm.Models.Add(new TaskModel { Count = group.Count, Probability = (double)group.Count / N, Name = group.Key.ToString() });
            }

        }

        private void AdditionalTask()
        {
            if (!dispatcherTimer.IsEnabled)
            {

                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
                double result = new Random().NextDouble();
                _wheelTime += (long)(result * 31.25);  // игра
                dispatcherTimer.Start();
            }
        }

        private void WheelEnd()
        {
            dispatcherTimer.Stop();
            _wheelCurrentTime = 0;
            _wheelSpeed = 20;


        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            var pieSeries = (this.WheelOfFortune.Diagram.Series[0] as PieSeries2D);
            if (pieSeries == null)
                return;
            double rotation = pieSeries.Rotation;
            rotation += _wheelSpeed;
            if (rotation >= 180)
            {
                rotation -= 360;
            }

            pieSeries.Rotation = rotation;
            _wheelCurrentTime++;

            if (_wheelCurrentTime >= _wheelTime)
            {
                WheelEnd();
            }

            if (_wheelCurrentTime >= 0.9 * _wheelTime)
            {
                _wheelSpeed = 1;
            }
            else if (_wheelCurrentTime >= 0.7 * _wheelTime)
            {
                _wheelSpeed = 2;
            }
            else if (_wheelCurrentTime >= 0.6 * _wheelTime)
            {
                _wheelSpeed = 5;
            }
            else if (_wheelCurrentTime >= 0.5 * _wheelTime)
            {
                _wheelSpeed = 10;
            }
            else if (_wheelCurrentTime >= 0.4 * _wheelTime)
            {
                _wheelSpeed = 15;
            }
        }

        private void ButtonTaskOne_Click(object sender, RoutedEventArgs e)
        {
            if (this.tasksList.SelectedIndex == 0)
                FirstTask();
            else if (this.tasksList.SelectedIndex == 1)
                SecondTask();
            else if (this.tasksList.SelectedIndex == 2)
                ThirdTask();
            else
                FourthTask();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTask();
        }
    }
}
