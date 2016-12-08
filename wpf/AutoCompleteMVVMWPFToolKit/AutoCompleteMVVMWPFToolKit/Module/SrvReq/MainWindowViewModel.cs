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
            //
            this.ItemSingle = new ViewModel.ObjectOptSingle<Item>();
            this.ItemSingle.OptArr.Add(new Item() { ID = 1, ItemNum = "CXR51A", ItemName = "Computer Regular Black" });
            this.ItemSingle.OptArr.Add(new Item() { ID = 2, ItemNum = "CXR52A", ItemName = "Computer Regular Red" });
            this.ItemSingle.OptArr.Add(new Item() { ID = 3, ItemNum = "CXR53A", ItemName = "Computer Regular Green" });

            this.IDItem = 3;

            //
            this.WarrantyMultiple = new ViewModel.ObjectOptMultiple<Warranty>();
            this.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 1, Text = "Send parts" });
            this.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 2, Text = "Return for repair" });
            this.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 3, Text = "Return for credit" });
            this.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 4, Text = "Create Invoice" });

            //
            this.CountrySingle = new ViewModel.ObjectOptSingle<Country>();
            this.CountrySingle.OptArr.Add(new Country() { ID = 1, Text = "Canada" });
            this.CountrySingle.OptArr.Add(new Country() { ID = 2, Text = "USA" });
            this.CountrySingle.OptArr.Add(new Country() { ID = 3, Text = "China" });
            this.CountrySingle.OptArr.Add(new Country() { ID = 4, Text = "Japan" });

            //
            this.DealerSingle = new ViewModel.ObjectOptSingle<Dealer>();
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 1, DealerName = "aaa 1", City = "city 1" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 2, DealerName = "aaa 2", City = "city 2" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 3, DealerName = "aaa 3", City = "city 3" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 4, DealerName = "bbb 4", City = "city 4" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 5, DealerName = "bbb 5", City = "city 5" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 6, DealerName = "bbb 6", City = "city 6" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 7, DealerName = "ccc 7", City = "city 7" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 8, DealerName = "ccc 8", City = "city 8" });
            this.DealerSingle.OptArr.Add(new Dealer() { ID = 9, DealerName = "ccc 9", City = "city 9" });

            this.IDDealer = 3;
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
