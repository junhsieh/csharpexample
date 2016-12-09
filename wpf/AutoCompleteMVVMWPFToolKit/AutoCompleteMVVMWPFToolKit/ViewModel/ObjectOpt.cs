using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoCompleteMVVMWPFToolKit.ViewModel
{
    class ObjectOpt<T>
    {
        public RangeEnabledObservableCollection<T> OptArr { get; set; }

        public ObjectOpt()
        {
            this.OptArr = new RangeEnabledObservableCollection<T>(); // TODO: need this line or not?
        }
    }

    class ObjectOptSingle<T> : ObjectOpt<T>
    {
        public int SelectedID
        {
            get
            {
                IEnumerable<ObjectOptBase> objArr = base.OptArr.Cast<ObjectOptBase>();

                foreach (ObjectOptBase obj in objArr)
                {
                    if (obj.IsSelected == true)
                    {
                        return obj.ID;
                    }
                }

                return 0;
            }
            set
            {
                IEnumerable<ObjectOptBase> objArr = base.OptArr.Cast<ObjectOptBase>();

                foreach (ObjectOptBase obj in objArr)
                {
                    obj.IsSelected = obj.ID == value ? true : false;
                }
            }
        }

        public T GetSelectedObj()
        {
            IEnumerable<ObjectOptBase> objArr = base.OptArr.Cast<ObjectOptBase>();

            int c = 0;

            foreach (ObjectOptBase obj in objArr)
            {
                if (obj.IsSelected == true)
                {
                    return base.OptArr[c];
                }

                c++;
            }

            return default(T);
        }
    }

    class ObjectOptMultiple<T> : ObjectOpt<T>
    {
        public List<int> SelectedID
        {
            get
            {
                IEnumerable<ObjectOptBase> objArr = base.OptArr.Cast<ObjectOptBase>();

                List<int> IDArr = new List<int>();

                foreach (ObjectOptBase obj in objArr)
                {
                    if (obj.IsSelected == true)
                    {
                        IDArr.Add(obj.ID);
                    }
                }

                return IDArr;
            }
            set
            {
                IEnumerable<ObjectOptBase> objArr = base.OptArr.Cast<ObjectOptBase>();

                foreach (ObjectOptBase obj in objArr)
                {
                    obj.IsSelected = value.Contains(obj.ID) == true ? true : false;
                }
            }
        }
    }

    class ObjectOptBase : ViewModel.ObservableBase
    {
        public int ID { get; set; }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                if (this._IsSelected != value)
                {
                    this._IsSelected = value;
                    base.NotifyPropertyChanged("IsSelected");
                }
            }
        }
    }
}
