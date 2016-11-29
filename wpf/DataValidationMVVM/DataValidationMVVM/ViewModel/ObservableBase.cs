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

        public void NotifyPropertyChanged(string propName)
        {
            // old way
            // NOTE: By storing a local copy of the event handler, we make sure to test and execute the same instance, hence solving the possible NullReferenceException.
            //PropertyChangedEventHandler _PropertyChanged = this.PropertyChanged;

            //if (_PropertyChanged != null)
            //{
            //    _PropertyChanged(this, new PropertyChangedEventArgs(propName));
            //    //_PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            //}

            // new way .Net 4.6
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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

        public IEnumerable GetErrors(string propName)
        {
            List<string> errArr;
            if (this.ErrorDict.TryGetValue(propName, out errArr) != true)
            {
                errArr = new List<string>();
            }
            return errArr;
        }

        public void NotifyErrorsChanged(string propName)
        {
            // old way
            //EventHandler<DataErrorsChangedEventArgs> _ErrorsChanged = this.ErrorsChanged;
            //if (_ErrorsChanged != null)
            //{
            //    _ErrorsChanged(this, new DataErrorsChangedEventArgs(propName));
            //    //_ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propName));
            //}

            // new way .Net 4.6
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
        }

        public void SetPropertyError(string propName, List<string> errArr)
        {
            this.ErrorDict[propName] = errArr;

            if (errArr.Count > 0)
            {
                this.NotifyErrorsChanged(propName);
            }
        }

        public List<string> GetClearedPropertyError(string propName)
        {
            List<string> errArr;

            if (this.ErrorDict.TryGetValue(propName, out errArr) != true)
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
