using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestFTP
{
    public class FtpWrapper
    {
        private readonly TcpListener _listener;
        private readonly IDataProcessor _dataProcessor;
        private readonly string _certificate;

        public FtpWrapper(IPAddress ipAddress, int port, IDataProcessor dataProcessor, string certificate)
        {
            _listener = new TcpListener(ipAddress, port);
            _dataProcessor = dataProcessor;
            _certificate = certificate;
        }

        public void Initialize()
        {
            _listener.Start();
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
            Console.WriteLine("Ftp Up and Running");
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            var tcpClient = _listener.EndAcceptTcpClient(result);
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);

            var stream = tcpClient.GetStream();
            
            var availableCommands = new List<IFtpCommand>
            {
                new ProtectionBufferSizePrivateCommand(),
                new ProtectionPrivateCommand(),
                new AuthTlsCommand(_certificate),
                new UserCommand(), 
                new PasswordCommand(),
                new KeepAliveCommand(),
                new ChangeWorkingDirectory(),
                new PrintWorkingDirectory(),
                new TypeCommand(),
                new PassiveCommand(),
                new ListCommand(),
                new StoreCommand(),
                new UnknownCommand()
            };


            var reader = new Reader(new StreamReader(stream, Encoding.ASCII));
            var writer = new Writer(new StreamWriter(stream, Encoding.ASCII));
            using (var client = new ClientStatus(tcpClient, writer, reader, _dataProcessor))
            {
                client.SendMessage("220 logged in");

                string line = null;

                while(!string.IsNullOrEmpty(line = client.ReadMessage()))
                {
                    Console.Write(line + " - ");

                    availableCommands.First(x => x.Handled(line)).DoOperation(line, client);
                }
            }
        }

        public bool Active()
        {
            return true;
        }
    }
}