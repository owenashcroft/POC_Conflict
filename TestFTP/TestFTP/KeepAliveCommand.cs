namespace TestFTP
{
    public class KeepAliveCommand : BaseCommand, IFtpCommand
    {
        public KeepAliveCommand()
            : base(new[] { "noop" })
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("200 ok");
        }
    }
}