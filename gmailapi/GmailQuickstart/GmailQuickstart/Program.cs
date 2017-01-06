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
            //ListLabels(service, UserId);
            //FetchMessageMain(service);

            GmailClient cli = new GmailClient();
            cli.ListLabels();

            Console.Read();
        }
    }
}