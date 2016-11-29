using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DataValidationIDataErrorInfoMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProductModel D { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.D = new ProductModel();
            this.DataContext = this.D;
        }

        private void CheckErrBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ERROR: " + this.D.Error);
        }
    }

    public class ProductModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // TODO: keep the errors of each field to a dictionary.
        private Dictionary<string, List<string>> ErrorDict = new Dictionary<string, List<string>>();
        
        private string _Error;
        public string Error
        {
            get { return _Error; }
            set
            {
                if (_Error != value)
                {
                    _Error = value;
                    //NotifyPropertyChanged("Error");
                }
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            result = "Name is required!";
                        break;
                    case "Price":
                        if ((Price < 10) || (Price > 100))
                            result = "Price must be between 10 and 100";
                        break;
                };
                this.Error = result;
                return result;
            }
        }

        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                NotifyPropertyChanged("Price");
            }
        }
    }
}
