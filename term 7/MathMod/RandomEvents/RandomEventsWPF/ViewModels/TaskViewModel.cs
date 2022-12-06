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
    public class TaskViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskModel> models = new ObservableCollection<TaskModel>();

        public List<GameModel> Games { get; set; } = new List<GameModel> { new GameModel { Name= "Witcher 3", Donuts = 1000},
            new GameModel{Name = "doKa 2 trade", Donuts = 1500}, 
            new GameModel { Name= "Witcher 2", Donuts = 1000}, 
            new GameModel { Name= "Witcher", Donuts = 2000},
        new GameModel { Name= "Skyrim", Donuts = 1000},
        new GameModel { Name= "Europa Universalis 4", Donuts = 1500},};
        public ObservableCollection<TaskModel> Models 
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
