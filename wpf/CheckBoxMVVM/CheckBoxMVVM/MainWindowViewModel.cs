using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CheckBoxMVVM
{
    public class MainWindowViewModel
    {
        public SalOrder SalOrder { get; set; }

        public MainWindowViewModel()
        {
            this.SalOrder = new SalOrder();
        }
    }

    public class SalOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public List<Int64> Product
        {
            get { return ViewModel.MultipleOptX.GetID(this.ProductOptArr); }
            set { ViewModel.MultipleOptX.SetID(this.ProductOptArr, value); }
        }
        public ObservableCollection<ViewModel.MultipleOpt> ProductOptArr { get; set; }

        public SalOrder()
        {
            this.ProductOptArr = new ObservableCollection<ViewModel.MultipleOpt>();
            this.ProductOptArr.Add(new ViewModel.MultipleOpt() { ID = 0, Text = "N/A" });
            this.ProductOptArr.Add(new ViewModel.MultipleOpt() { ID = 1, Text = "Computer" });
            this.ProductOptArr.Add(new ViewModel.MultipleOpt() { ID = 2, Text = "Desk" });
            this.ProductOptArr.Add(new ViewModel.MultipleOpt() { ID = 3, Text = "Table" });
        }
    }
}
