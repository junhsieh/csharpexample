using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DataValidationMVVM.ViewModel
{
    class ObservableBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Implementing INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Implementing INotifyDataErrorInfo
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
            if (this.ErrorDict.TryGetValue(propertyName, out errArr) != true)
            {
                errArr = new List<string>();
            }
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

        public string GetAllErrorStr(string[] fieldArr)
        {
            List<string> errArr = new List<string>();

            foreach (string field in fieldArr)
            {
                IEnumerable<string> eArr = this.GetErrors(field).Cast<string>();

                if (eArr.Count<string>() > 0)
                {
                    errArr.Add(field + ": " + String.Join("; ", eArr));
                }
            }

            return String.Join("\n", errArr);
        }
        #endregion
    }
}
