using System.Collections.Generic;
using System.Configuration;
using System.Net;
using TestWCFProxy;

namespace TestFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];
            var url = ConfigurationManager.AppSettings["WebApiUrl"];
            var ftp = new FtpWrapper(IPAddress.Any, 21, new DataProcessor(baseAddress, url));
            ftp.Initialize();
            while (ftp.Active()){ }
        }
    }
}
