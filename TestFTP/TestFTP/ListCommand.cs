using System.Collections.Generic;

namespace TestFTP
{
    public class ListCommand : BaseCommand, IFtpCommand
    {
        public ListCommand()
            : base(new[] { "list" })
        {
            
        }

        public void DoOperation(string command, ClientStatus clientStatus)
        {
            clientStatus.DataConnection.SendDirectoryListing();
            clientStatus.SendMessage("150 opening connection for list");
        }
    }
}