using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DataValidationMVVM.ViewModel
{
    class ObservableBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /*
         * Implementing INotifyPropertyChanged
         */
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /*
         * Implementing INotifyDataErrorInfo
         */
        private Dictionary<string, List<string>> ErrorDict = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                return this.ErrorDict.Values.Any((l) => l.Count > 0);
                //return ErrorDict.Values.FirstOrDefault((l) => l.Count > 0) != null;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errArr;
            this.ErrorDict.TryGetValue(propertyName, out errArr);
            return errArr;
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
            {
                this.ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public void SetPropertyError(string propertyName, List<string> errArr)
        {
            this.ErrorDict[propertyName] = errArr;

            if (errArr.Count > 0)
            {
                this.RaiseErrorsChanged(propertyName);
            }
        }

        public List<string> GetClearedPropertyError(string propertyName)
        {
            List<string> errArr;

            if (this.ErrorDict.TryGetValue(propertyName, out errArr) != true)
            {
                errArr = new List<string>();
            }
            else
            {
                errArr.Clear();
            }

            return errArr;
        }

        public string GetErrorStr(string propertyName)
        {
            return String.Join("; ", GetErrors(propertyName).Cast<string>());
        }
    }
}
