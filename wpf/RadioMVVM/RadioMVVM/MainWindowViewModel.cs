using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RadioMVVM
{
    class MainWindowViewModel
    {
        public SalOrder SalOrder { get; set; }

        public MainWindowViewModel()
        {
            this.SalOrder = new SalOrder();
        }
    }

    class SalOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Int64 Country
        {
            get { return ViewModel.SingleOptX.GetID(this.CountryOptArr); }
            set { ViewModel.SingleOptX.SetID(this.CountryOptArr, value); }
        }
        public ObservableCollection<ViewModel.SingleOpt> CountryOptArr { get; set; }

        public SalOrder()
        {
            this.CountryOptArr = new ObservableCollection<ViewModel.SingleOpt>();
            this.CountryOptArr.Add(new ViewModel.SingleOpt() { ID = 0, Text = "N/A" });
            this.CountryOptArr.Add(new ViewModel.SingleOpt() { ID = 1, Text = "Canada" });
            this.CountryOptArr.Add(new ViewModel.SingleOpt() { ID = 2, Text = "USA" });
            this.CountryOptArr.Add(new ViewModel.SingleOpt() { ID = 3, Text = "Japan" });
        }
    }
}
