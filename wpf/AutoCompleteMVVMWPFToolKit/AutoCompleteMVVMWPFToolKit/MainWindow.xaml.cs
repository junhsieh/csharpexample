using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

namespace AutoCompleteMVVMWPFToolKit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AutoCompleteMVVMWPFToolKit.MainWindow mw;
        internal Lib.HTTPData httpdata;

        public MainWindow()
        {
            InitializeComponent();

            mw = (AutoCompleteMVVMWPFToolKit.MainWindow)Application.Current.MainWindow;

            httpdata = new Lib.HTTPData()
            {
                Cookie = new CookieContainer(),
                CSRFtoken = "",
            };

            IDItemBox.SelectionChanged += IDItemBox_SelectionChanged;
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

            if (i.Status == true)
            {
                // Get the first CSRF token.
                i = Lib.Util.GetWebContent(ref this.httpdata, "/CSRFToken");

                if (i.Status == true)
                {
                    // do something
                }
                else
                {
                    MessageBox.Show(String.Join(";", i.ErrArr));
                }
            }
            else
            {
                MessageBox.Show(String.Join(";", i.ErrArr));
            }
        }

        private void InitIDItem()
        {
            Lib.iojson i = Lib.Util.GetWebContent(ref mw.httpdata, "/Item/ListAC");
            List<ItemAC> itemArr = new List<ItemAC>();
            i.GetObjFromArr(0, itemArr);
            this.MainWindowViewModel.SalOrder.SetIDItemOptArr(itemArr);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Login();

            InitIDItem();
        }

        private void IDItemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ObjectOpt item = (ViewModel.ObjectOpt)((AutoCompleteBox)sender).SelectedItem;

            if (item != null)
            {
                var test = (ItemAC)item.ID;
                Debug.WriteLine("HERE: " + test.IDItem + "_" + test.ItemNum + "_" + test.ItemName);
            }
        }
    }

    class LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    class ItemAC
    {
        public Int64 IDItem { get; set; }
        public string ItemNum { get; set; }
        public string ItemName { get; set; }
    }
}
