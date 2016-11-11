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

namespace CheckBoxTextBox
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
            //this.txtCompleteName.Text = "Jun Hsieh";
        }

        private void SetBtn_Click_1(object sender, RoutedEventArgs e)
        {
            this.txtCompleteName.Text = "False True False";
        }

        private void Set2Btn_Click_1(object sender, RoutedEventArgs e)
        {
            this.txtCompleteName.Text = "True True False";
        }
    }

    public class CustomMultiValueConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return String.Join(" ", values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            List<object> outArr = new List<object>();
            var valArr = (value as string).Split(' ');

            foreach (var item in valArr)
            {
                outArr.Add(System.Convert.ToBoolean(item));
            }

            return outArr.ToArray();
            //return (value as string).Split(' ');
            //return new object[] { true, false, true };
        }
    }

}
