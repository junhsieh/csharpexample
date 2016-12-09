using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoCompleteMVVMWPFToolKit.Module.SrvReq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AutoCompleteMVVMWPFToolKit.MainWindow mw;

        public MainWindow()
        {
            InitializeComponent();

            mw = (AutoCompleteMVVMWPFToolKit.MainWindow)Application.Current.MainWindow;
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

        private void UploadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.MainWindowViewModel.SrvReqCore.UploadFileMultiple.OptArr.Count > 5)
            {
                MessageBox.Show("You have exceeded the maximum number of the upload file limit.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            //openFileDialog.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG" + "|All files (*.*)|*.*";
            openFileDialog.Filter = "Images (*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.PDF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.PDF";

            if (openFileDialog.ShowDialog() == true && openFileDialog.FileNames.Length > 0)
            {
                if (openFileDialog.FileNames.Length > 5)
                {
                    MessageBox.Show("You have exceeded the maximum number of the upload file limit.");
                    return;
                }

                Lib.iojson i = Lib.Util.UploadFile(ref mw.httpdata, "/UploadFile/Save", openFileDialog.FileNames);

                if (i.Status != true)
                {
                    MessageBox.Show(String.Join(",", i.ErrArr));
                }
                else
                {
                    List<UploadFile> fileArr = new List<UploadFile>();
                    i.GetObjFromArr(0, fileArr);

                    InitUploadFile(fileArr);
                }
            }
        }

        private void InitUploadFile(List<UploadFile> fileArr)
        {
            foreach (var file in fileArr)
            {
                this.MainWindowViewModel.SrvReqCore.UploadFileMultiple.OptArr.Add(new UploadFile()
                {
                    ID = 1,
                    FileName = file.FileName,
                    FileSize = 999,
                    FileMime = "image/png",
                    IsSelected = true,
                });
            }
        }

        private void CheckBox_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            string fullFilePath = Lib.Util.GetWebSrvURL() + "/uploadfile/srvreq/" + cb.Content.ToString();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            cb.ToolTip = new Image() { Source = bitmap, Width = 600, Height = 400, Stretch = Stretch.Uniform };
        }
    }
}
