using System;
using System.Windows;

namespace RadioMVVM
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

        private void SetCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            // set
            this.MainWindowViewModel.SalOrder.Country = Int32.Parse(this.IDCountryBox.Text);

            // show
            ShowSelectedCountry();
        }

        private void ShowCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectedCountry();
        }

        private void ShowSelectedCountry()
        {
            this.ShowCountryLb.Content = this.MainWindowViewModel.SalOrder.Country.ToString() + ": "
                + ViewModel.SingleOptX.GetText(this.MainWindowViewModel.SalOrder.CountryOptArr);
        }
    }
}
