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
                    RaisePropertyChanged("Name");
                    Validate();
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
                    RaisePropertyChanged("Name");
                    Validate();
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
                //Validate Name
                List<string> errorsForName;
                if (!_errors.TryGetValue("Name", out errorsForName))
                    errorsForName = new List<string>();
                else
                    errorsForName.Clear();

                if (String.IsNullOrEmpty(Name))
                    errorsForName.Add("The name could not be empty.");
                if (Name != "a")
                    errorsForName.Add("The name must be a.");

                _errors["Name"] = errorsForName;
                if (errorsForName.Count > 0)
                    RaiseErrorsChanged("Name");
            }
        }
    }
}
