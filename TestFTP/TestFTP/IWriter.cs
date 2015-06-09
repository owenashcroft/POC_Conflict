using System;

namespace TestFTP
{
    public interface IWriter : IDisposable
    {
        void Send(string message);
    }
}