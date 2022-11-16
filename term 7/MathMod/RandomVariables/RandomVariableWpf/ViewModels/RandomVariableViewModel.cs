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

        private ObservableCollection<TaskModel> histData { get; set; } = new ObservableCollection<TaskModel>();

        public ObservableCollection<TaskModel> HistData { 
            get { return histData; } 
            set { 
                histData = value; 
                OnPropertyChanged(); }  }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
