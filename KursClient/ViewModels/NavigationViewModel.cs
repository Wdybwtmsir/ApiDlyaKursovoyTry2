using KursClient.Models;
using KursClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KursClient.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand HomeCommand { get; set; }
        public ICommand ClientCommand { get; set; }
        public ICommand NumbersOtherCommand { get; set; }
        
        private void HomeView(object obj) => CurrentView = new HomeViewModel();
        private void ClientView(object obj) => CurrentView = new ClientViewModel();
        private void NumbersOtherView(object obj) => CurrentView = new NumbersOtherViewModel();
        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(HomeView);
            ClientCommand = new RelayCommand(ClientView);
            NumbersOtherCommand = new RelayCommand(NumbersOtherView);
            CurrentView = new HomeViewModel();
        }
    }
}