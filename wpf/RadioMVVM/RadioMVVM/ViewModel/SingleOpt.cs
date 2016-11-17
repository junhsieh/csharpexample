using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace RadioMVVM.ViewModel
{
    class SingleOpt : INotifyPropertyChanged
    {
        public Int64 ID { get; set; }
        public string Text { get; set; }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                if (this._IsSelected != value)
                {
                    this._IsSelected = value;
                    this.NotifyPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    static class SingleOptX
    {
        public static Int64 GetID(ObservableCollection<SingleOpt> optArr)
        {
            return optArr.Where(d => d.IsSelected == true).Select(d => d.ID).Skip(0).Take(1).FirstOrDefault<Int64>();
        }

        public static void SetID(ObservableCollection<SingleOpt> optArr, Int64 value)
        {
            optArr.ToList().ForEach(d => d.IsSelected = false);
            optArr.Where(d => d.ID == value).ToList().ForEach(d => d.IsSelected = true);
        }

        public static string GetText(ObservableCollection<SingleOpt> optArr)
        {
            string str = "";

            if (optArr != null)
            {
                str = optArr.Where(d => d.IsSelected == true).Select(d => d.Text).Skip(0).Take(1).FirstOrDefault<string>();
            }

            return str;
        }

        public static void RemoveOptArr(ObservableCollection<SingleOpt> optArr)
        {
            ObservableCollection<SingleOpt> tmpArr = new ObservableCollection<SingleOpt>(optArr.Where(d => d.IsSelected == false));

            foreach (var item in tmpArr)
            {
                optArr.Remove(item);
            }
        }
    }
}
