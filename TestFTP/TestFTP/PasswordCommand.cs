namespace TestFTP
{
    public class PasswordCommand : BaseCommand, IFtpCommand
    {
        public PasswordCommand() : base("pass")
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("220 logged in");
        }
    }
}