using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LicenseServer.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var webClient = new WebClient();
            var down = true;
            webClient.QueryString = new NameValueCollection() { { "appKey", "112734" } };
            webClient.DownloadStringTaskAsync("http://localhost:9679/api/license/validaApp");
            webClient.DownloadStringCompleted += (e, a) =>
            {
                down = false;
                Console.WriteLine(e);
                Console.WriteLine(a.Result);

            };
            while (down)
            {
                Console.Write(".");
                Thread.Sleep(2000);
            }
            Console.ReadKey();
        }
    }
}
