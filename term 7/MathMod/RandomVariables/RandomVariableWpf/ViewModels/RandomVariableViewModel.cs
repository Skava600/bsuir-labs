using RandomVariableWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RandomVariableWpf.ViewModels
{
    internal class RandomVariableViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<double> uniformData { get; set; } = new ObservableCollection<double>();
        public int Intervals {get;} = 30;
        public ObservableCollection<double> UniformData { 
            get { return uniformData; } 
            set { 
                uniformData = value; 
                OnPropertyChanged(); }  }

        private ObservableCollection<TaskModel> negBinData { get; set; } = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> NegBinData
        {
            get { return negBinData; }
            set
            {
                negBinData = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
