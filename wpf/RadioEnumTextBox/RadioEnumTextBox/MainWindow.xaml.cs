using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace RadioEnumTextBox
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

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {
            //ColorBox.Text = ((int)Colors.Green).ToString();
            ColorBox.Text = (2).ToString();

            Debug.WriteLine("ColorBox: " + ColorBox.Text);
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ColorBox: " + ColorBox.Text);
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

            return value.Equals(((int)parameter).ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return value.Equals(true) ? (int)Enum.Parse(typeof(Colors), parameter.ToString()) : Binding.DoNothing;
            return value.Equals(true) ? (int)parameter : Binding.DoNothing;
        }
    }

}
