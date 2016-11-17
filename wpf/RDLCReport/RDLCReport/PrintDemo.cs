using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Text;
using System.Threading;

namespace RDLCReport
{
    public class PrintDemo : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private string PDFPath = "";
        private string PrinterName = "";

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
        }

        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
            {
                throw new Exception("Error: no stream to print.");
            }

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = this.PrinterName;

            if (this.PrinterName == "Microsoft Print to PDF")
            {
                var directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var file = (string)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                this.PDFPath = Path.Combine(directory, file + ".pdf");

                printDoc.PrinterSettings.PrintToFile = true;
                printDoc.PrinterSettings.PrintFileName = this.PDFPath;
            }

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public string GetDefaultPrinterNameV1()
        {
            PrinterSettings settings = new PrinterSettings();
            return settings.PrinterName;
        }

        // this one seems more reliable.
        public string GetDefaultPrinterNameV2()
        {
            var query = new ObjectQuery("SELECT * FROM Win32_Printer");
            var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (((bool?)mo["Default"]) ?? false)
                {
                    return mo["Name"] as string;
                }
            }

            return null;
        }

        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run(string RDLCFile, string DataSetName, DataTable data, string PrinterName)
        {
            LocalReport report = new LocalReport();
            report.ReportPath = RDLCFile;
            report.DataSources.Add(new ReportDataSource(DataSetName, data));

            // basic data
            List<ReportParameter> MyParam1 = new List<ReportParameter>();
            MyParam1.Add(new ReportParameter("MyParam1", "Computer"));
            MyParam1.Add(new ReportParameter("MyParam1", "Monitor"));
            ReportParameter MyParam2 = new ReportParameter("MyParam2", "Keyboard");

            report.SetParameters(MyParam1);
            report.SetParameters(MyParam2);

            Export(report);

            this.PrinterName = PrinterName;
            this.Print();

            if (this.PrinterName == "Microsoft Print to PDF")
            {
                this.OpenPDF();
            }
        }

        private void OpenPDF()
        {
            Debug.WriteLine(this.PDFPath);

            Thread.Sleep(1500);
            System.Diagnostics.Process.Start(this.PDFPath);
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                {
                    stream.Close();
                }
                m_streams = null;
            }
        }
    }
}
