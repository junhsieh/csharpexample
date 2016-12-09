using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteMVVMWPFToolKit.ViewModel
{
    class RangeEnabledObservableCollection<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> items)
        {
            this.CheckReentrancy();

            foreach (var item in items)
            {
                this.Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void AddRangeBegin(IEnumerable<T> items)
        {
            this.Items.Clear();

            this.AddRange(items);
        }
    }
}
