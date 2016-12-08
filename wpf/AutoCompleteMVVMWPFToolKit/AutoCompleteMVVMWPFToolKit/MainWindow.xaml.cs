using System.Windows;

namespace AutoCompleteMVVMWPFToolKit
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

        private void SrvReqBtn_Click(object sender, RoutedEventArgs e)
        {
            Module.SrvReq.MainWindow w = new Module.SrvReq.MainWindow();

            w.Owner = Window.GetWindow(this);
            w.ShowDialog();
        }
    }
}
