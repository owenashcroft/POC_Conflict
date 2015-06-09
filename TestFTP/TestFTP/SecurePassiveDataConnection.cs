using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TestFTP
{
    public class SecurePassiveDataConnection : BasePassiveDataConnection, IFtpDataConnection
    {
        private readonly X509Certificate _certificate;
        private readonly IDataProcessor _dataProcessor;

        public SecurePassiveDataConnection(IPAddress listeningIpAddress, int port, IWriter writer, X509Certificate certificate, 
            IDataProcessor dataProcessor)
            :base(listeningIpAddress, port, writer)
        {
            _certificate = certificate;
            _dataProcessor = dataProcessor;
        }

        public void SendDirectoryListing()
        {
            _passiveListener.BeginAcceptTcpClient(ListDirectoryContents, "");
        }

        private void ListDirectoryContents(IAsyncResult ar)
        {
            var dataClient = _passiveListener.EndAcceptTcpClient(ar);
            dataClient.Close();
            _writer.Send("226 Secure Directory Transfer Complete");
        }

        public void SendFile(string fileName)
        {
            _passiveListener.BeginAcceptTcpClient(ReceiveFile, fileName);
        }

        private void ReceiveFile(IAsyncResult result)
        {
            var fileName = result.AsyncState as string;

            Console.WriteLine("File {0} being transferred", fileName);

            var dataClient = _passiveListener.EndAcceptTcpClient(result);
            var resultFromClient = new StringBuilder();
            using (var dataStream = new SslStream(dataClient.GetStream()))
            {
                dataStream.AuthenticateAsServer(_certificate);
                using (var reader = new StreamReader(dataStream))
                {
                    

                    while (!reader.EndOfStream)
                    {
                        var buffer = new char[1024];

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
            if (_dataProcessor.ProcessData(ConvertToStringAndRemoveExcessBufferCrap(resultFromClient)))
            {
                _writer.Send("226 file transferred correctly!");
               return;
            }

            _writer.Send("451 validation error!");
        }

        private static string ConvertToStringAndRemoveExcessBufferCrap(StringBuilder resultFromClient)
        {
            return resultFromClient.ToString().Trim().Trim('\0');
        }
    }
}