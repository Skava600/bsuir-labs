using RandomEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomEventsWPF.ViewModels
{
    public class TaskOneViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskOneModel> models = new ObservableCollection<TaskOneModel>();
        public ObservableCollection<TaskOneModel> Models 
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
}

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
