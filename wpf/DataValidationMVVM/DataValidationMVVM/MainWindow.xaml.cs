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
        }

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable errArr = this.MainWindowViewModel.Person.GetErrors("Name");

            Debug.WriteLine(DateTime.Now + " HasError: " + this.MainWindowViewModel.Person.HasErrors);

            if (this.MainWindowViewModel.Person.HasErrors == true)
            {
                Debug.WriteLine(DateTime.Now + " HERE: " + String.Join(", ", errArr.Cast<string>()));
            }
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
