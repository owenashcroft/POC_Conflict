namespace TestFTP
{
    internal interface IFtpCommand
    {
        void DoOperation(string command, ClientStatus clientStatus);

        bool Handled(string command);
    }
}