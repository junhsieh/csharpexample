using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;

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
            //this.MainWindowViewModel.Person.Name = "jun";
            //this.MainWindowViewModel.Person.Age = 19;
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
