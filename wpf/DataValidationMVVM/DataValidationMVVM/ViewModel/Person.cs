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
                    base.RaisePropertyChanged("Name");
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
                    base.RaisePropertyChanged("Name");
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
                // Name
                List<string> errorsForName = base.GetClearedPropertyError("Name");

                if (String.IsNullOrEmpty(this.Name))
                    errorsForName.Add("The name could not be empty.");
                if (this.Name != "a")
                    errorsForName.Add("The name must be a.");

                base.SetPropertyError("Name", errorsForName);
            }
        }
    }
}
