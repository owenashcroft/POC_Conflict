using System;
using System.Threading;
using System.Web.Http;
using MassTransit;
using MassTransit.BusConfigurators;
using TestWCFProxy;

namespace TestWebServiceThing.Controllers
{
    public class MainController : ApiController
    {
        [HttpGet]
        public string Default()
        {
            return "Hello world!";
        }

        [HttpPost]
        public bool Submit(ClientData[] clientData)
        {
            var bus = CreateBus("TestSender", x => {  });

            foreach (var message in clientData)
            {
                bus.Publish(message, publishContext =>
                {
                    publishContext.SetHeader("Header 1","Header 1 Value");
                    publishContext.SetDeliveryMode(DeliveryMode.Persistent);
                });
                Thread.Sleep(1);
            }
            
            bus.Dispose();

            return true;
        }

        public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
        {
            var bus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://localhost/TestSender");
                moreInitialization(x);
            });

            return bus;
        }
    }
}
