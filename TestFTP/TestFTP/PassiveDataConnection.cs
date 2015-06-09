using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestFTP
{
    public interface IFtpDataConnection : IDisposable
    {
        void Start();
        void SendDirectoryListing();
        void SendFile(string fileName);
    }

    public class PassiveDataConnection : BasePassiveDataConnection, IFtpDataConnection
    {
        private readonly IDataProcessor _dataProcessor;

        public PassiveDataConnection(IPAddress listeningIpAddress, int port, IWriter writer, IDataProcessor dataProcessor) 
            : base(listeningIpAddress, port, writer)
        {
            _dataProcessor = dataProcessor;
        }

        public void SendDirectoryListing()
        {
            _passiveListener.BeginAcceptTcpClient(ListDirectoryContents, "");
        }

        public void SendFile(string fileName)
        {
            _passiveListener.BeginAcceptTcpClient(ReceiveFile, fileName);
        }

        private void ReceiveFile(IAsyncResult result)
        {
            var fileName = result.AsyncState as string;

            Console.WriteLine("File {0} being transfer", fileName);
            
            var dataClient = _passiveListener.EndAcceptTcpClient(result);
            var resultFromClient = new StringBuilder();

            using (var dataStream = dataClient.GetStream())
            {
                using (var reader = new StreamReader(dataStream, Encoding.ASCII))
                {
                    var buffer = new char[1024];
                    
                    while (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                        reader.ReadBlock(buffer, 0, buffer.Length);
                        
                        resultFromClient.Append(buffer);
                    }
                }
            }

            ProcessData(resultFromClient);

            dataClient.Close();

        }

        private void ProcessData(StringBuilder resultFromClient)
        {
            if (_dataProcessor.ProcessData(resultFromClient.ToString()))
            {
                _writer.Send("226 file transferred correctly!");
                return;
            }
            
            _writer.Send("451 validation error!");
        }

        private void ListDirectoryContents(IAsyncResult result)
        {
            var dataClient = _passiveListener.EndAcceptTcpClient(result);
            dataClient.Close();
            _writer.Send("226 Directory Transfer Complete");
        }
    }
}