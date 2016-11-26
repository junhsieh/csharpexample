using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataValidationMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.MainWindowViewModel.Person.Name = "jun2";
            //this.MainWindowViewModel.Person.Age = 9;

            // Note: this is for catching the exception. Ex: binding an integer to a TextBox.
            // make sure you do add these in XAML: ValidatesOnExceptions=True, NotifyOnValidationError=True
            //this.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnValidationRaised));
        }

        private void OnValidationRaised(object sender, RoutedEventArgs e)
        {
            var args = (System.Windows.Controls.ValidationErrorEventArgs)e;

            //var test = ((Control)e.OriginalSource).Name;
            //Debug.WriteLine(DateTime.Now + " HERE: " + test);

            //DependencyObject dpobj = sender as DependencyObject;
            //string name = dpobj.GetValue(FrameworkElement.NameProperty) as string;

            Binding myBinding = BindingOperations.GetBinding(((Control)e.OriginalSource), TextBox.TextProperty);

            if (this.MainWindowViewModel != null)
            {
                // Check if the error was caused by an exception
                if (args.Error.RuleInError is ExceptionValidationRule)
                {
                    // Add or remove the error from the ViewModel
                    if (args.Action == ValidationErrorEventAction.Added)
                    {
                        //this.MainWindowViewModel.AddUIValidationError();
                    }
                    else if (args.Action == ValidationErrorEventAction.Removed)
                    {
                        //this.MainWindowViewModel.RemoveUIValidationError();
                    }
                }
            }
        }

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(DateTime.Now + " HasError: " + this.MainWindowViewModel.Person.HasErrors);

            if (this.MainWindowViewModel.Person.HasErrors == true)
            {
                IEnumerable errArrForName = this.MainWindowViewModel.Person.GetErrors("Name");
                if (errArrForName != null)
                    Debug.WriteLine(DateTime.Now + " Name: " + String.Join(", ", errArrForName.Cast<string>()));

                IEnumerable errArrForAge = this.MainWindowViewModel.Person.GetErrors("Age");
                if (errArrForAge != null)
                    Debug.WriteLine(DateTime.Now + " Age: " + String.Join(", ", errArrForAge.Cast<string>()));
            }

            Debug.WriteLine(DateTime.Now + " NameText: " + this.MainWindowViewModel.Person.Name);
            Debug.WriteLine(DateTime.Now + " AgeText: " + this.MainWindowViewModel.Person.Age.ToString());
            Debug.WriteLine("");
        }
    }

    class MainWindowViewModel
    {
        public ViewModel.Person Person { get; set; }

        public MainWindowViewModel()
        {
            this.Person = new ViewModel.Person();
        }
    }
}
