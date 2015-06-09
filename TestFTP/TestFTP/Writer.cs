using System;
using System.IO;

namespace TestFTP
{
    public class Writer : IWriter
    {
        private readonly StreamWriter _writer;

        public Writer(StreamWriter writer)
        {
            _writer = writer;
        }

        public void Send(string message)
        {
            lock (_writer)
            {
                try
                {
                    Console.WriteLine(message + "\n");
                    _writer.WriteLine(message);
                    _writer.Flush();
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("Writer closed, client has shut connection");
                }
            }
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}