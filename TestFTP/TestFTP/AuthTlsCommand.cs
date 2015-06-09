using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace TestFTP
{
    public class AuthTlsCommand : BaseCommand, IFtpCommand
    {
        public AuthTlsCommand() : base("auth tls")
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("234 TLS connection");
            var cert = new X509Certificate(@"c:\temp\server.cer");
            clientStatus.MakeSecure(cert);
        }
    }
}