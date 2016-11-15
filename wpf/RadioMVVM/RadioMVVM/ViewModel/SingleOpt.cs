using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioMVVM.ViewModel
{
    public class SingleOpt : INotifyPropertyChanged
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

    public static class SingleOptX
    {
        public static Int64 GetID(ObservableCollection<SingleOpt> optArr)
        {
            return optArr.Where(d => d.IsSelected == true).Select(d => d.ID).Skip(0).Take(1).FirstOrDefault<Int64>();
        }

        public static void SetID(ObservableCollection<SingleOpt> optArr, Int64 value)
        {
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
    }
}
