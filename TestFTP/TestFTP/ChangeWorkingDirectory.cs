namespace TestFTP
{
    public class ChangeWorkingDirectory : BaseCommand, IFtpCommand
    {
        public ChangeWorkingDirectory()
            : base(new[] { "cwd" })
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("250 ok");
        }
    }
}