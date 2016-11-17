using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AutoCompleteWPFToolKit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.DealerNameArr.Add(new Dealer() { IDDealer = 0, DealerName = "Dealer 0"});
            //this.DealerNameArr.Add(new Dealer() { IDDealer = 1, DealerName = "Dealer 1"});
            //this.DealerNameArr.Add(new Dealer() { IDDealer = 2, DealerName = "Dealer 2"});

            //MyAC.ItemsSource = DealerNameArr;

            //this.MyAC.PreviewKeyDown += new KeyEventHandler( (a, b) =>
            this.MyAC.Populating += new PopulatingEventHandler((a, b) =>
            {
                /*
                switch (b.Key)
                {
                    case Key.Delete:
                    case Key.Back:
                    case Key.Up:
                    case Key.Down:
                    case Key.Left:
                    case Key.Right:
                    case Key.Home:
                    case Key.End:
                        return;
                }
                */
                //var k = ConvertKeyToChar(b.Key);

                List<Dealer> DealerNameArr = new List<Dealer>();

                for (int i = 0; i < 10; i++)
                {
                    DealerNameArr.Add(new Dealer() { IDDealer = i, DealerName = "dealer " + i.ToString()});
                }

                MyAC.ItemsSource = DealerNameArr;
                //this.MyAC.ItemsSource.IsDropDownOpen = true;
            }
            );
        }

        public static char ConvertKeyToChar(Key key)
        {
            return Convert.ToChar(KeyInterop.VirtualKeyFromKey(key));
        }

        private void MyAC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((AutoCompleteBox)sender).SelectedItem;
            Dealer d = (Dealer)item;
            textBlock.Text = d.DealerName + "_" + d.IDDealer;
        }
    }

    public class Dealer
    {
        public int IDDealer { get; set; }
        public string DealerName { get; set; }
    }
}
