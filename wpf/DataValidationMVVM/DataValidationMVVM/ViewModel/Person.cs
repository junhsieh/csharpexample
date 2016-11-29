using System;
using System.Collections.Generic;

namespace DataValidationMVVM.ViewModel
{
    class Person : ObservableBase
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    base.NotifyPropertyChanged("Name");
                    Validate("Name");
                }
            }
        }

        private int _Age = 0;
        public int Age
        {
            get { return _Age; }
            set
            {
                if (_Age != value)
                {
                    _Age = value;
                    base.NotifyPropertyChanged("Age");
                    Validate("Age");
                }
            }
        }

        private string _Errors;
        public string Errors
        {
            get
            {
                string[] fieldArr = new string[] { "Name", "Age" };
                return base.HasErrors == true ? base.GetAllErrorStr(fieldArr) : "";
            }
            set
            {
                _Errors = value;
                base.NotifyPropertyChanged("Errors");
            }
        }
        private void SetErrors()
        {
            string[] fieldArr = new string[] { "Name", "Age" };
            this.Errors = base.HasErrors == true ? base.GetAllErrorStr(fieldArr) : "";
        }

        private void Validate(string propertyName)
        {
            //int WaitSecondsBeforeValidation = 2;
            //Task waitTask = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(WaitSecondsBeforeValidation)));
            //waitTask.ContinueWith((_) => RealValidation());
            //waitTask.Start();

            RealValidation(propertyName);
        }

        private object _lock = new object();
        private void RealValidation(string propertyName)
        {
            lock (_lock)
            {
                List<string> errArr = base.GetClearedPropertyError(propertyName);

                switch (propertyName)
                {
                    case "Name":
                        if (String.IsNullOrEmpty(this.Name))
                            errArr.Add("The name could not be empty.");
                        if (this.Name != "jun")
                            errArr.Add("The name must be jun.");
                        break;
                    case "Age":
                        if (String.IsNullOrEmpty(this.Age.ToString()))
                            errArr.Add("The Age could not be empty.");
                        if (this.Age < 18)
                            errArr.Add("Age must be over 18.");
                        break;
                }

                base.SetPropertyError(propertyName, errArr);
                this.SetErrors();
            }
        }
    }
}
