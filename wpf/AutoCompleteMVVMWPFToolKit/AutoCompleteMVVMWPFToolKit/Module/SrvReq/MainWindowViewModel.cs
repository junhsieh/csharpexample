using Newtonsoft.Json;
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
        public int IDItem { get { return ItemSingle.SelectedID; } set { ItemSingle.SelectedID = value; } }
        [JsonIgnore]
        public ViewModel.ObjectOptSingle<Item> ItemSingle { get; set; }

        // Warranty
        public List<int> IDWarranty
        {
            get { return WarrantyMultiple.SelectedID; }
            set { WarrantyMultiple.SelectedID = value; }
        }
        [JsonIgnore]
        public ViewModel.ObjectOptMultiple<Warranty> WarrantyMultiple { get; set; }

        // Country
        public int IDCountry
        {
            get { return CountrySingle.SelectedID; }
            set
            {
                CountrySingle.SelectedID = value;
                base.NotifyPropertyChanged("IDCountry"); // need this if it is a ComboBox.
            }
        }
        [JsonIgnore]
        public ViewModel.ObjectOptSingle<Country> CountrySingle { get; set; }

        // Dealer
        public int IDDealer
        {
            get { return DealerSingle.SelectedID; }
            set
            {
                DealerSingle.SelectedID = value;
                base.NotifyPropertyChanged("Dealer"); // need this if it a AutocompleteBox.
            }
        }
        [JsonIgnore]
        public ViewModel.ObjectOptSingle<Dealer> DealerSingle { get; set; }
        [JsonIgnore]
        public Dealer Dealer
        {
            get { return DealerSingle.GetSelectedObj(); }
            set
            {
                if (value != null)
                {
                    this.IDDealer = value.ID;
                }
            }
        }
        [JsonIgnore]
        public AutoCompleteFilterPredicate<object> DealerFilter
        {
            get
            {
                AutoCompleteFilterPredicate<object> result = (searchText, obj) =>
                {
                    var item = (Dealer)obj;

                    if (obj != null)
                    {
                        if (item.DealerName.ToLower().Contains(searchText.ToLower()))
                        {
                            return true;
                        }
                        else if (item.City.ToLower().Contains(searchText.ToLower()))
                        {
                            return true;
                        }
                    }
                    return false;
                };

                return result;
            }
        }

        // Person
        public int IDPerson
        {
            get { return PersonSingle.SelectedID; }
            set
            {
                PersonSingle.SelectedID = value;
                base.NotifyPropertyChanged("Person"); // need this if it a AutocompleteBox.
            }
        }
        [JsonIgnore]
        public ViewModel.ObjectOptSingle<Person> PersonSingle { get; set; }
        [JsonIgnore]
        public Person Person
        {
            get { return PersonSingle.GetSelectedObj(); }
            set
            {
                if (value != null)
                {
                    this.IDPerson = value.ID;
                }
            }
        }
        [JsonIgnore]
        public AutoCompleteFilterPredicate<object> PersonFilter
        {
            get
            {
                AutoCompleteFilterPredicate<object> result = (searchText, obj) =>
                {
                    var item = (Person)obj;

                    if (obj != null)
                    {
                        if (item.PersonName.ToLower().Contains(searchText.ToLower()))
                        {
                            return true;
                        }
                        else if (item.City.ToLower().Contains(searchText.ToLower()))
                        {
                            return true;
                        }
                    }
                    return false;
                };

                return result;
            }
        }

        // UploadFile
        public List<int> IDUploadFile
        {
            get { return UploadFileMultiple.SelectedID; }
            set { UploadFileMultiple.SelectedID = value; }
        }
        [JsonIgnore]
        public ViewModel.ObjectOptMultiple<UploadFile> UploadFileMultiple { get; set; }

        public SrvReqCore()
        {
            this.ItemSingle = new ViewModel.ObjectOptSingle<Item>();
            this.WarrantyMultiple = new ViewModel.ObjectOptMultiple<Warranty>();
            this.CountrySingle = new ViewModel.ObjectOptSingle<Country>();
            this.DealerSingle = new ViewModel.ObjectOptSingle<Dealer>();
            this.PersonSingle = new ViewModel.ObjectOptSingle<Person>();
            this.UploadFileMultiple = new ViewModel.ObjectOptMultiple<UploadFile>();
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

    class Person : ViewModel.ObjectOptBase
    {
        public string PersonName { get; set; }
        public string City { get; set; }
    }

    class UploadFile : ViewModel.ObjectOptBase
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileMime { get; set; }
    }
}
