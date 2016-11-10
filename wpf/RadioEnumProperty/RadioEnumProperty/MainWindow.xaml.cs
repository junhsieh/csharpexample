using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace RadioEnumProperty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Car MyCar { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.MyCar = new Car();
            this.MyCar.Color = Colors.Red;
            this.MyCarSP.DataContext = MyCar;
        }

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MyCar.Color = Colors.Green;
            Debug.WriteLine(this.MyCar.Color);
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(this.MyCar.Color);
        }
    }

    public class Car : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private Colors color { get; set; }
        public Colors Color
        {
            get { return this.color; }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.NotifyPropertyChanged("Color");
                }
            }
        }
    }

    public enum Colors
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
    };

    public class EnumToIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Debug.WriteLine("Type 1: " + value.GetType());
            //Debug.WriteLine("Type 2: " + parameter.GetType());
            //Debug.WriteLine("Type 3: " + Type.GetTypeCode(targetType));
            //Debug.WriteLine("Type 4: " + TypeDescriptor.GetConverter(targetType));

            //string checkValue = ((int)Enum.Parse(typeof(Colors), (string)parameter)).ToString();
            //return value.Equals(checkValue);

            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return value.Equals(true) ? (int)Enum.Parse(typeof(Colors), parameter.ToString()) : Binding.DoNothing;
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
