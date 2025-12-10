using KursClient.Models;
using KursClient.Services;
using KursClient.Utils;
using KursClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursClient.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private ClientService clientService;
        private ObservableCollection<Client> clientList;
        public ObservableCollection<Client> ClientList
        {
            get { return clientList; }
            set
            {
                if (value!=null)
                {
                    clientList = value;
                    OnPropertyChanged(nameof(ClientList));
                }
            }
        }
        public ClientViewModel()
        {
            clientService = new ClientService();
            Load();
        }
        private void Load()
        {
            try
            {
                ClientList = null!;
                Task<List<Client>> task = Task.Run(() => clientService.GetAll());
                ClientList = new ObservableCollection<Client>(task.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async obj =>
                  {
                      try
                      {
                          AddEditClient window = new AddEditClient(new Client());
                          if (window.ShowDialog() == true)
                          {
                              Client client = new Client();
                              client.FirstName = window.Clien.FirstName;
                              client.LastName = window.Clien.LastName;
                              client.SurName = window.Clien.SurName;
                              client.NumberOfClientRegistration = window.Clien.NumberOfClientRegistration;
                              client.TypeOfDocument = window.Clien.TypeOfDocument;
                              client.SerialAndNumberOfDocument = window.Clien.SerialAndNumberOfDocument;
                              client.BirthDay = window.Clien.BirthDay;
                              client.Sex = window.Clien.Sex;
                              client.HomeAddress = window.Clien.HomeAddress;   
                              client.NumberOfRoom = window.Clien.NumberOfRoom;
                              await clientService.Add(client);
                              Load();
                          }
                      }
                      catch { }
                  }));
            }
        }

    }
}