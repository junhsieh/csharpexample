using System.Collections.Generic;
using System.Windows;

namespace AutoCompleteMVVMWPFToolKit.Module.SrvReq
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

        private void SetItemBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindowViewModel.SrvReqCore.IDItem = 2;
        }

        private void SetWarrantyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindowViewModel.SrvReqCore.IDWarranty = new List<int>() { 1, 3 };
        }

        private void SetCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindowViewModel.SrvReqCore.IDCountry = 3;
        }

        private void ShowValueBtn_Click(object sender, RoutedEventArgs e)
        {
            Lib.iojson i = new Lib.iojson();

            i.AddObjToArr(this.MainWindowViewModel);

            DebugBox.Text = i.Encode();
        }

        private void SetDealerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindowViewModel.SrvReqCore.IDDealer = 5;
        }
    }
}
