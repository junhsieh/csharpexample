using System.Collections.Generic;
using System.Windows.Controls;

namespace AutoCompleteMVVMWPFToolKit.Module.SrvReq
{
    class MainWindowViewModel
    {
        public SrvReqCore SrvReqCore { get; set; }

        public MainWindowViewModel()
        {
            this.SrvReqCore = new SrvReqCore();
        }
    }

    class SrvReqCore : ViewModel.ObservableBase
    {
        // Item
        public int IDItem
        {
            get { return ItemSingle.SelectedID; }
            set { ItemSingle.SelectedID = value; }
        }
        public ViewModel.ObjectOptSingle<Item> ItemSingle { get; set; }

        // Warranty
        public List<int> IDWarranty
        {
            get { return WarrantyMultiple.SelectedID; }
            set { WarrantyMultiple.SelectedID = value; }
        }
        public ViewModel.ObjectOptMultiple<Warranty> WarrantyMultiple { get; set; }

        // Country
        public int IDCountry
        {
            get { return CountrySingle.SelectedID; }
            set
            {
                CountrySingle.SelectedID = value;
                base.NotifyPropertyChanged("IDCountry");
            }
        }
        public ViewModel.ObjectOptSingle<Country> CountrySingle { get; set; }

        // Dealer
        public int IDDealer
        {
            get { return DealerSingle.SelectedID; }
            set
            {
                DealerSingle.SelectedID = value;
                base.NotifyPropertyChanged("Dealer");
            }
        }
        public ViewModel.ObjectOptSingle<Dealer> DealerSingle { get; set; }
        public Dealer Dealer
        {
            get
            {
                return DealerSingle.GetSelectedObj();
            }
            set
            {
                if (value != null)
                {
                    this.IDDealer = value.ID;
                }
            }
        }
        public AutoCompleteFilterPredicate<object> DealerFilter
        {
            get
            {
                AutoCompleteFilterPredicate<object> result = (searchText, obj) =>
                {
                    var item = (Dealer)obj;

                    if (obj != null)
                    {
                        if (item.DealerName.ToLower().Contains(searchText))
                        {
                            return true;
                        }
                        else if (item.City.ToLower().Contains(searchText))
                        {
                            return true;
                        }
                    }
                    return false;
                };

                return result;
            }
        }

        public SrvReqCore()
        {
            this.ItemSingle = new ViewModel.ObjectOptSingle<Item>();
            this.WarrantyMultiple = new ViewModel.ObjectOptMultiple<Warranty>();
            this.CountrySingle = new ViewModel.ObjectOptSingle<Country>();
            this.DealerSingle = new ViewModel.ObjectOptSingle<Dealer>();
        }
    }

    class Item : ViewModel.ObjectOptBase
    {
        public string ItemNum { get; set; }
        public string ItemName { get; set; }
    }

    class Warranty : ViewModel.ObjectOptBase
    {
        public string Text { get; set; }
    }

    class Country : ViewModel.ObjectOptBase
    {
        public string Text { get; set; }
    }

    class Dealer : ViewModel.ObjectOptBase
    {
        public string DealerName { get; set; }
        public string City { get; set; }
    }
}
