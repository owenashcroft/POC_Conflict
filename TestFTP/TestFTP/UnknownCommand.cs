namespace TestFTP
{
    public class UnknownCommand: IFtpCommand
    {
        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("502 unknown");
        }

        public bool Handled(string command)
        {
            return true;
        }
    }
}