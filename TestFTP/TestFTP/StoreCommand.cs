namespace TestFTP
{
    public class StoreCommand : BaseCommand, IFtpCommand
    {
        public StoreCommand() : base(new [] { "stor" })
        {
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.DataConnection.SendFile(command.ToLower().Replace("stor", ""));
            clientStatus.SendMessage("150 ready for transfer");
        }
    }
}