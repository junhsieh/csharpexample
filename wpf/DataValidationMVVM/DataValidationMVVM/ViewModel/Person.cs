using System;
using System.Collections.Generic;

namespace DataValidationMVVM.ViewModel
{
    class Person : ObservableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Validate();
                    base.RaisePropertyChanged("Name");
                }
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    Validate();
                    base.RaisePropertyChanged("Age");
                }
            }
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
                if (this.Name != "a")
                    errArrForName.Add("The name must be a.");

                base.SetPropertyError("Name", errArrForName);

                // Age
                List<string> errArrForAge = base.GetClearedPropertyError("Age");

                if (this.Age < 18)
                    errArrForAge.Add("Age must be over 18.");

                base.SetPropertyError("Age", errArrForAge);
            }
        }
    }
}
