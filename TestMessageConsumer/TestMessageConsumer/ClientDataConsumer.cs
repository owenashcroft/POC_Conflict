using System;
using System.Configuration;
using MassTransit;
using TestWCFProxy;

namespace TestMessageConsumer
{
    public class ClientDataConsumer : Consumes<ClientData>.Context
    {
        private readonly IDataSink _dataSink;

        public ClientDataConsumer()
        {
            _dataSink = new SqlDataSink(ConfigurationManager.ConnectionStrings["database"]);
        }

        public void Consume(IConsumeContext<ClientData> message)
        {
            _dataSink.Save(message.Message);

            Console.WriteLine("Message processed");
        }
    }
}
