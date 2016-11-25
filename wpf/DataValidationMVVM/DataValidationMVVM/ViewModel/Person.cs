using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
                    Validate();
                    base.RaisePropertyChanged("Name");
                }
            }
        }

        private int _Age;
        public int Age
        {
            get { return _Age; }
            set
            {
                if (_Age != value)
                {
                    _Age = value;
                    Validate();
                    base.RaisePropertyChanged("Age");
                }
            }
        }

        public string Errors
        {
            get { return base.HasErrors == true ? base.GetErrorStr("Name") : ""; }
        }

        private void Validate()
        {
            //int WaitSecondsBeforeValidation = 2;
            //Task waitTask = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(WaitSecondsBeforeValidation)));
            //waitTask.ContinueWith((_) => RealValidation());
            //waitTask.Start();

            RealValidation();
        }

        private object _lock = new object();
        private void RealValidation()
        {
            lock (_lock)
            {
                // Name
                List<string> errArrForName = base.GetClearedPropertyError("Name");

                if (String.IsNullOrEmpty(this.Name))
                    errArrForName.Add("The name could not be empty.");
                if (this.Name != "jun")
                    errArrForName.Add("The name must be jun.");

                base.SetPropertyError("Name", errArrForName);
                //this.NameErr = base.GetErrorStr("Name");

                // Age
                List<string> errArrForAge = base.GetClearedPropertyError("Age");

                if (this.Age < 18)
                    errArrForAge.Add("Age must be over 18.");

                base.SetPropertyError("Age", errArrForAge);

                // Errors
                base.RaisePropertyChanged("Errors");
            }
        }
    }
}
