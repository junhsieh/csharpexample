using System;
using System.Linq;
using System.Windows;

namespace CheckBoxMVVM
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

        private void SetProductBtn_Click(object sender, RoutedEventArgs e)
        {
            // set
            if (this.IDProductBox.Text.Length > 0)
            {
                string str = this.IDProductBox.Text;
                this.MainWindowViewModel.SalOrder.Product = str.Split(',').Select(Int64.Parse).ToList();
            }

            // show
            ShowSelectedProduct();
        }

        private void ShowProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectedProduct();
        }

        private void ShowSelectedProduct()
        {
            this.ShowProductLb.Content = String.Join(", ", this.MainWindowViewModel.SalOrder.Product) + ": "
                + String.Join(", ", ViewModel.MultipleOptX.GetText(this.MainWindowViewModel.SalOrder.ProductOptArr));
        }
    }
}
