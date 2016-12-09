using AutoCompleteMVVMWPFToolKit.Module.SrvReq;
using System;
using System.Configuration;
using System.Net;
using System.Windows;

namespace AutoCompleteMVVMWPFToolKit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Lib.HTTPData httpdata;

        public MainWindow()
        {
            InitializeComponent();

            SrvReqBtn.IsEnabled = false;

            httpdata = new Lib.HTTPData()
            {
                Cookie = new CookieContainer(),
                CSRFtoken = "",
            };
        }

        private void Login()
        {
            Lib.iojson o = new Lib.iojson();
            Lib.iojson i = new Lib.iojson();

            LoginInfo li = new LoginInfo()
            {
                Username = ConfigurationManager.AppSettings["Username"],
                Password = ConfigurationManager.AppSettings["Password"],
            };
            o.AddObjToArr(li);
            i = Lib.Util.PostWebContent(ref this.httpdata, "/Login", o.Encode());

            if (i.Status != true)
            {
                MessageBox.Show(String.Join("; ", i.ErrArr));
            }
            else
            {
                // Get the first CSRF token.
                i = Lib.Util.GetWebContent(ref this.httpdata, "/CSRFToken");

                if (i.Status != true)
                {
                    MessageBox.Show(String.Join("; ", i.ErrArr));
                }
                else
                {
                    SrvReqBtn.IsEnabled = true;
                }
            }
        }

        private void SrvReqBtn_Click(object sender, RoutedEventArgs e)
        {
            Module.SrvReq.MainWindow w = new Module.SrvReq.MainWindow();
            w.Owner = Window.GetWindow(this);

            w.MainWindowViewModel.SrvReqCore.ItemSingle.OptArr.Add(new Item() { ID = 1, ItemNum = "CXR51A", ItemName = "Computer Regular Black" });
            w.MainWindowViewModel.SrvReqCore.ItemSingle.OptArr.Add(new Item() { ID = 2, ItemNum = "CXR52A", ItemName = "Computer Regular Red" });
            w.MainWindowViewModel.SrvReqCore.ItemSingle.OptArr.Add(new Item() { ID = 3, ItemNum = "CXR53A", ItemName = "Computer Regular Green" });

            w.MainWindowViewModel.SrvReqCore.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 1, Text = "Send parts" });
            w.MainWindowViewModel.SrvReqCore.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 2, Text = "Return for repair" });
            w.MainWindowViewModel.SrvReqCore.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 3, Text = "Return for credit" });
            w.MainWindowViewModel.SrvReqCore.WarrantyMultiple.OptArr.Add(new Warranty() { ID = 4, Text = "Create Invoice" });

            w.MainWindowViewModel.SrvReqCore.CountrySingle.OptArr.Add(new Country() { ID = 1, Text = "Canada" });
            w.MainWindowViewModel.SrvReqCore.CountrySingle.OptArr.Add(new Country() { ID = 2, Text = "USA" });
            w.MainWindowViewModel.SrvReqCore.CountrySingle.OptArr.Add(new Country() { ID = 3, Text = "China" });
            w.MainWindowViewModel.SrvReqCore.CountrySingle.OptArr.Add(new Country() { ID = 4, Text = "Japan" });

            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 1, DealerName = "aaa 1", City = "city 1" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 2, DealerName = "aaa 2", City = "city 2" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 3, DealerName = "aaa 3", City = "city 3" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 4, DealerName = "bbb 4", City = "city 4" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 5, DealerName = "bbb 5", City = "city 5" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 6, DealerName = "bbb 6", City = "city 6" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 7, DealerName = "ccc 7", City = "city 7" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 8, DealerName = "ccc 8", City = "city 8" });
            w.MainWindowViewModel.SrvReqCore.DealerSingle.OptArr.Add(new Dealer() { ID = 9, DealerName = "ccc 9", City = "city 9" });

            w.ShowDialog();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Login();
        }
    }

    class LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
