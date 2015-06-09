namespace TestFTP
{
    public class PassiveCommand : BaseCommand, IFtpCommand
    {
        public PassiveCommand() : base( "pasv" )
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.StartPassiveListener();
        }
    }

    public class ProtectionPrivateCommand : BaseCommand, IFtpCommand
    {
        public ProtectionPrivateCommand() : base("prot p")
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("220 ok");
        }
    }

    public class ProtectionBufferSizePrivateCommand : BaseCommand, IFtpCommand
    {
        public ProtectionBufferSizePrivateCommand()
            : base("pbsz")
        {

        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("220 ok");
        }
    }
}