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

namespace ComboBoxMVVM
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

        private void SetProvinceBtn_Click(object sender, RoutedEventArgs e)
        {
            // set
            this.MainWindowViewModel.SalOrder.Province = Int32.Parse(this.IDProvinceBox.Text);

            // show
            ShowSelectedProvince();
        }

        private void ShowProvinceBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectedProvince();
        }

        private void ShowSelectedProvince()
        {
            this.ShowProvinceLb.Content = this.MainWindowViewModel.SalOrder.Province.ToString() + ": "
                + ViewModel.SingleOptX.GetText(this.MainWindowViewModel.SalOrder.ProvinceOptArr);
        }
    }
}
