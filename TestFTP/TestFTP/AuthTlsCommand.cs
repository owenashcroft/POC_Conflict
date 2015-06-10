using System.Security.Cryptography.X509Certificates;

namespace TestFTP
{
    public class AuthTlsCommand : BaseCommand, IFtpCommand
    {
        private readonly string _certificate;

        public AuthTlsCommand(string certificate) : base("auth tls")
        {
            _certificate = certificate;
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("234 TLS connection");
            var cert = new X509Certificate2(_certificate, "password");
            clientStatus.MakeSecure(cert);
        }
    }
}