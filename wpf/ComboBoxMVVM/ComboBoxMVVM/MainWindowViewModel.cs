using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboBoxMVVM
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

        public Int64 Province
        {
            get { return ViewModel.SingleOptX.GetID(this.ProvinceOptArr); }
            set {
                ViewModel.SingleOptX.SetID(this.ProvinceOptArr, value);
                NotifyPropertyChanged("Province"); // NOTE: for ComboBox, it needs to be "notified" manually.
            }
        }
        public ObservableCollection<ViewModel.SingleOpt> ProvinceOptArr { get; set; }

        public SalOrder()
        {
            this.ProvinceOptArr = new ObservableCollection<ViewModel.SingleOpt>();
            this.ProvinceOptArr.Add(new ViewModel.SingleOpt() { ID = 0, Text = "N/A" });
            this.ProvinceOptArr.Add(new ViewModel.SingleOpt() { ID = 1, Text = "BC" });
            this.ProvinceOptArr.Add(new ViewModel.SingleOpt() { ID = 2, Text = "AB" });
            this.ProvinceOptArr.Add(new ViewModel.SingleOpt() { ID = 3, Text = "MB" });
        }
    }
}
