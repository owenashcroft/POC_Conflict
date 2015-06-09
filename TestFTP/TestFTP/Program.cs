using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace TestFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];
            var url = ConfigurationManager.AppSettings["WebApiUrl"];
            var certificate = ConfigurationManager.AppSettings["CertificateLocation"];
            var absoluteCertificatePath = Path.GetFullPath(certificate);
            var validation = new FiveColumnOnlyValidation();
            var ipAddress = IPAddress.Parse(ConfigurationManager.AppSettings["IpAddress"]);
            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            
            Console.WriteLine("Setting up on IP Address {0}", ipAddress);
            Console.WriteLine("Setting up on port {0}", port);
            Console.WriteLine("Loading Certificate from {0}", absoluteCertificatePath);
            
            var ftp = new FtpWrapper(ipAddress, port, new DataProcessor(baseAddress, url, validation), absoluteCertificatePath);
            ftp.Initialize();
            while (ftp.Active()){ }
        }
    }
}
