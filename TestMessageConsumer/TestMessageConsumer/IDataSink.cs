using System;
using TestWCFProxy;

namespace TestMessageConsumer
{
    public interface IDataSink
    {
        void Save(ClientData message);
    }
}

