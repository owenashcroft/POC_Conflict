using System;
using System.Net;
using System.Net.Sockets;

namespace TestFTP
{
    public abstract class BasePassiveDataConnection : IDisposable
    {
        protected readonly int _port;
        protected readonly IWriter _writer;
        protected readonly IPAddress _listeningIpAddress;
        protected TcpListener _passiveListener;

        protected BasePassiveDataConnection(IPAddress listeningIpAddress, int port, IWriter writer)
        {
            _listeningIpAddress = listeningIpAddress;
            _port = port;
            _writer = writer;
        }

        public virtual void Start()
        {
            _passiveListener = new TcpListener(_listeningIpAddress, _port);

            _passiveListener.Start();

            var localEndpoint = ((IPEndPoint)_passiveListener.LocalEndpoint);

            var address = localEndpoint.Address.GetAddressBytes();
            var assignedPort = (short)localEndpoint.Port;

            var portArray = BitConverter.GetBytes(assignedPort);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(portArray);

            _writer.Send(string.Format("227 Entering Passive Mode ({0},{1},{2},{3},{4},{5})",
                address[0], address[1], address[2], address[3], portArray[0], portArray[1]));
        }

        public void Dispose()
        {
            if (_passiveListener != null)
                _passiveListener.Stop();
        }
    }
}