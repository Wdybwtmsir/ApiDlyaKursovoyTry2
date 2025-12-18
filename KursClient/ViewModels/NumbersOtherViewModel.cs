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
    public class NumbersOtherViewModel : ViewModelBase
    {
        private NumbersOtherService numbersOtherService;
        private ObservableCollection<NumbersOther> numbersOtherList;
        public ObservableCollection<NumbersOther> NumbersOtherList
        {
            get { return numbersOtherList; }
            set
            {
                if (numbersOtherList != value)
                {
                    numbersOtherList = value;
                    OnPropertyChanged(nameof(NumbersOtherList));
                }
            }
        }
        public NumbersOtherViewModel()
        {
            numbersOtherService = new NumbersOtherService();
            Load();
        }
        private void Load()
        {
            try
            {
                NumbersOtherList = null!;
                Task<List<NumbersOther>> task = Task.Run(() => numbersOtherService.GetAll());
                NumbersOtherList = new ObservableCollection<NumbersOther>(task.Result);
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
                          AddEditNumbersOther window = new AddEditNumbersOther(new NumbersOther());
                          if (window.ShowDialog() == true)
                          {
                              NumbersOther numbersOther = new NumbersOther();
                              numbersOther.NumberOfRoom = window.NumbersOth.NumberOfRoom;
                              numbersOther.TypeOfNumber = window.NumbersOth.TypeOfNumber;
                              numbersOther.CountOfMest = window.NumbersOth.CountOfMest;
                              numbersOther.Floor=window.NumbersOth.Floor;
                              numbersOther.Phone = window.NumbersOth.Phone;
                              numbersOther.CostPerDay=window.NumbersOth.CostPerDay;
                              numbersOther.CountOfFreePlaces = window.NumbersOth.CountOfFreePlaces;
                              numbersOther.IdClient = window.NumbersOth.IdClient;
                              await numbersOtherService.Add(numbersOther);

                              Load();
                          }
                      } 
                      catch { }
                  }));
            }
        }

    }
}