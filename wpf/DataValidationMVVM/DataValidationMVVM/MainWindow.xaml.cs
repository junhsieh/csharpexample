using System;
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
            var test = this.MainWindowViewModel.Person.GetErrors("Name");

            if (test != null)
            {
                var str = String.Join(", ", test.Cast<string>());
                Debug.WriteLine(DateTime.Now + " HERE: " + str);
            }
            else
            {
                Debug.WriteLine(DateTime.Now + " HERE: null");
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
