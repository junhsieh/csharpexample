using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CheckBoxProperty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public MyData MyData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //MyData = new MyData();
        }

        private void ShowLanguageBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MyData.showLanguage();
        }

        private void SetLanguageBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MyData.setLanguage(LanguageBox.Text);
        }
    }

    public class MyData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private ObservableCollection<Language> _Language { get; set; }
        //public ObservableCollection<Language> Language
        //{
        //    get { return _Language; }
        //    set
        //    {
        //        if (_Language != value)
        //        {
        //            _Language = value;
        //            NotifyPropertyChanged("Language");
        //        }
        //    }
        //}
        public ObservableCollection<Language> Language { get; set; }

        public MyData()
        {
            initLanguage();
        }

        public void initLanguage()
        {
            this.Language = new ObservableCollection<Language>();
            this.Language.Add(new Language() { ID = 1, Text = "C" });
            this.Language.Add(new Language() { ID = 2, Text = "PHP" });
            this.Language.Add(new Language() { ID = 3, Text = "Go" });
            this.Language.Add(new Language() { ID = 4, Text = "CSharp" });
        }

        public void setLanguage(string IDs)
        {
            if (IDs.Length > 0)
            {
                IEnumerable<int> IDArr = IDs.Split(',').Select(x => int.Parse(x));

                //this.Language
                //    .Where(d => IDArr.Contains(d.ID))
                //    .ToList().ForEach(d => d.IsSelected = true);

                foreach (var item in this.Language)
                {
                    if (IDArr.Contains<int>(item.ID))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                }
                showLanguage();
            }
        }

        public void showLanguage()
        {
            IEnumerable<string> selectedData = this.Language.Where(d => d.IsSelected).Select(d => d.Text);

            foreach (var item in selectedData)
            {
                Debug.WriteLine("Language HERE: " + item);
            }

            Debug.WriteLine("");
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class Language : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }
        public string Text { get; set; }

        private bool _IsSelected { get; set; }
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
