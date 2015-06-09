using System.Collections.Generic;
using System.Net;
using TestWCFProxy;

namespace TestFTP
{
    class Program
    {
        private static int _recordsPerSubmission = 50000;

        static void Main(string[] args)
        {
            using (var proxy = new TestProjectProxy())
            {
                proxy.SubmitNewClientInformation(new List<ClientData>());
            }

            var ftp = new FtpWrapper(IPAddress.Any, 21, new DataProcessor(new TestProjectProxy(), _recordsPerSubmission));
            ftp.Initialize();
            while (ftp.Active()){ }
        }
    }
}
