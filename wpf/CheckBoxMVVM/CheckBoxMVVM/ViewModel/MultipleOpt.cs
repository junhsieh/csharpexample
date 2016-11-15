using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CheckBoxMVVM.ViewModel
{
    public class MultipleOpt : INotifyPropertyChanged
    {
        public Int64 ID { get; set; }
        public string Text { get; set; }
        private bool _IsSelected;
        public bool IsSelected { get { return this._IsSelected; } set { if (this._IsSelected != value) { this._IsSelected = value; this.NotifyPropertyChanged("IsSelected"); } } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    public static class MultipleOptX
    {
        public static List<Int64> GetID(ObservableCollection<MultipleOpt> optArr)
        {
            var IDArr = optArr.Where(d => d.IsSelected == true).Select(d => d.ID).ToList<Int64>();
            return IDArr.Count == 0 ? null : IDArr;
        }

        public static void SetID(ObservableCollection<MultipleOpt> optArr, List<Int64> value)
        {
            if (value != null)
            {
                optArr.Where(d => value.Contains(d.ID)).ToList().ForEach(d => d.IsSelected = true);
            }
        }

        public static List<string> GetText(ObservableCollection<MultipleOpt> optArr)
        {
            List<string> strArr = null;

            if (optArr != null)
            {
                strArr = optArr.Where(d => d.IsSelected == true).Select(d => d.Text).ToList<string>();
            }

            return strArr;
        }
    }
}
