using KursClient.Models;
using KursClient.Services;
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
using System.Windows.Shapes;

namespace KursClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditNumbersOther.xaml
    /// </summary>
    public partial class AddEditNumbersOther : Window
    {
        private ClientService _clientService;
        private async Task Load()
        {
            ClientList.ItemsSource = await _clientService.GetAll();
        }

        public NumbersOther NumbersOth { get; private set; }
        public AddEditNumbersOther(NumbersOther _numbersOther)
        {
            NumbersOth = _numbersOther;
            DataContext = NumbersOth;
            _clientService = new ClientService();
            Load();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
