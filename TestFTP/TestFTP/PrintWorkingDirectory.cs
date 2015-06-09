namespace TestFTP
{
    public class PrintWorkingDirectory : BaseCommand, IFtpCommand
    {
        public PrintWorkingDirectory() : base(new[] { "pwd" })
        {
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("257 \"/\"");
        }
    }
}