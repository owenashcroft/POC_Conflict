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
            var certificate = ConfigurationManager.AppSettings["CertificateLocation"];
            var ftp = new FtpWrapper(IPAddress.Any, 21, new DataProcessor(baseAddress, url), certificate);
            ftp.Initialize();
            while (ftp.Active()){ }
        }
    }
}
