using System;
using MassTransit;
using MassTransit.BusConfigurators;

namespace TestMessageConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver: Listening to bus...");
            
            var bus = CreateBus("TestReceiver", busConfigurator =>
            {
                busConfigurator.Subscribe(subs =>
                {
                    subs.Consumer<ClientDataConsumer>().Permanent();
                });
            });

            Console.ReadLine();

            bus.Dispose();
        }

        public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
        {
            var bus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://localhost/TestReceiver");
                moreInitialization(x);
            });

            return bus;
        }
    }
}
