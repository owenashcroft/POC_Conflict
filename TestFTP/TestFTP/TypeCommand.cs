namespace TestFTP
{
    public class TypeCommand : BaseCommand, IFtpCommand
    {
        public TypeCommand() : base(new [] {"type"})
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.SendMessage("200 ok");
        }
    }
}