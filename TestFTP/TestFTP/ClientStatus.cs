using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace TestFTP
{
    public class ClientStatus : IDisposable
    {
        private readonly TcpClient _client;
        private IWriter _writer;
        private IReader _reader;
        private IFtpDataConnection _dataConnection;
        private X509Certificate _cert;
        private readonly IDataProcessor _dataProcessor;

        public ClientStatus(TcpClient client, IWriter writer, IReader reader, IDataProcessor dataProcessor)
        {
            _client = client;
            _writer = writer;
            _reader = reader;
            _dataProcessor = dataProcessor;
        }

        public void StartPassiveListener()
        {
            StartPassiveListener(0);
        }

        public void StartPassiveListener(int port)
        {
            var localAddress = ((IPEndPoint)_client.Client.LocalEndPoint).Address;

            if (_cert == null)
            {
                _dataConnection = new PassiveDataConnection(localAddress, port, _writer, _dataProcessor);
                _dataConnection.Start();
                return;
            }

            _dataConnection = new SecurePassiveDataConnection(localAddress, port, _writer, _cert, _dataProcessor);
            _dataConnection.Start();
        }

        public void CloseDataConnection()
        {
            _dataConnection.Dispose();
        }

        public void Dispose()
        {
            if (_dataConnection != null)
                _dataConnection.Dispose();

            _writer.Dispose();
        }

        public void SendMessage(string message)
        {
            _writer.Send(message);
        }

        public string ReadMessage()
        {
            return _reader.Read();
        }

        public IFtpDataConnection DataConnection { get {  return _dataConnection; } }

        public void MakeSecure(X509Certificate cert)
        {
            _cert = cert;
            var sslStream = WrapStream(cert);
            _writer = new Writer(new StreamWriter(sslStream));
            _reader = new Reader(new StreamReader(sslStream));
        }

        private SslStream WrapStream(X509Certificate cert)
        {
            var stream = _client.GetStream();
            var sslStream = new SslStream(stream);
            sslStream.AuthenticateAsServer(cert);
            return sslStream;
        }
    }
}