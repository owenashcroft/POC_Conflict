namespace TestFTP
{
    public class UserCommand : BaseCommand, IFtpCommand
    {
        public void DoOperation(string command, ClientStatus clientStatus)
        {
            if (!Handled(command))
                return;

            clientStatus.SendMessage("331 ok"); 
        }

        public UserCommand() : base(new [] { "user" })
        {
        }
    }
}