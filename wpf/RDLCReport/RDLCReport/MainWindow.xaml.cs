using Microsoft.Reporting.WinForms;
using System.Data;
using System.Windows;

namespace RDLCReport
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

        private void ShowReportBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportDataSource.Value = this.GetData();

            this.PrintDoc.Reset();

            this.PrintDoc.LocalReport.ReportEmbeddedResource = "RDLCReport.Report1.rdlc";
            this.PrintDoc.LocalReport.DataSources.Add(reportDataSource);

            this.PrintDoc.RefreshReport();
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("firstname", typeof(string)));
            dt.Columns.Add(new DataColumn("lastname", typeof(string)));
            dt.Columns.Add(new DataColumn("age", typeof(string)));

            for (int i = 0; i < 16; i++)
            {
                if (i == 3)
                {
                    DataRow dr = dt.NewRow();
                    dr["firstname"] = "jun " + i;
                    dr["lastname"] = "hsieh " + i + "\n" + "test" + "\n" + "test";
                    dr["age"] = "18";
                    dt.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["firstname"] = "jun " + i;
                    dr["lastname"] = "hsieh " + i;
                    dr["age"] = "18";
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }

    }

    class Person
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
    }
}
