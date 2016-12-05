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
        public ItemAC IDItem
        {
            get { return ViewModel.ObjectOptSingleUtil.GetID<ItemAC>(this.IDItemOptArr); }
            set
            {
                ViewModel.ObjectOptSingleUtil.SetID<ItemAC>(this.IDItemOptArr, value);
                base.NotifyPropertyChanged("IDItem");
            }
        }
        private ObservableCollection<ViewModel.ObjectOpt> _IDItemOptArr;
        public ObservableCollection<ViewModel.ObjectOpt> IDItemOptArr
        {
            get { return _IDItemOptArr; }
            set
            {
                _IDItemOptArr = value;
                base.NotifyPropertyChanged("IDItemOptArr");
            }
        }

        public SalOrder()
        {
            this.IDItemOptArr = new ObservableCollection<ViewModel.ObjectOpt>();
        }

        public void SetIDItemOptArr(List<ItemAC> itemArr)
        {
            this.IDItemOptArr.Clear();

            //
            ObservableCollection<ViewModel.ObjectOpt> oc = new ObservableCollection<ViewModel.ObjectOpt>();

            itemArr.ForEach((d) => oc.Add(new ViewModel.ObjectOpt() { ID = d, Text = d.ItemName }));

            this.IDItemOptArr = oc;
        }
    }
}
