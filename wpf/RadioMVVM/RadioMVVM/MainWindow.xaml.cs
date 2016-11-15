using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
