using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteMVVMWPFToolKit
{
    class MainWindowViewModel
    {
        public SalOrder SalOrder { get; set; }

        public MainWindowViewModel()
        {
            this.SalOrder = new SalOrder();
        }
    }

    class SalOrder : ViewModel.ObservableBase
    {
        public string ItemName { get { return ViewModel.ObjectOptSingleUtil.GetText(this.IDItemOptArr); } }
        public Int64 IDItem
        {
            get { return ViewModel.ObjectOptSingleUtil.GetID<Int64>(this.IDItemOptArr); }
            set {
                ViewModel.ObjectOptSingleUtil.SetID<Int64>(this.IDItemOptArr, value);
                base.NotifyPropertyChanged("IDItem");
            }
        }
        public ObservableCollection<ViewModel.ObjectOpt> IDItemOptArr { get; set; }
    }
}
