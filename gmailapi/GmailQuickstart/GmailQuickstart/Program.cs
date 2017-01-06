using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GmailQuickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            GmailClient cli = new GmailClient();

            if (cli.IsValidCredential == true)
            {
                //cli.ListLabels();
                cli.ProcessMessageMain();
            }

            Console.Read();
        }
    }
}