using System;
using MassTransit;
using MassTransit.BusConfigurators;
using TestWCFProxy;

namespace TestSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver: One");

//            Bus.Initialize(sbc =>
//            {
//                sbc.UseRabbitMq();
//                sbc.ReceiveFrom("rabbitmq://localhost/ClientDataReceiver");
//
//                sbc.Subscribe(subs =>
//                {
//                    var del = new Action<IConsumeContext<ClientData>, ClientData>((context, msg) =>
//                    {
//                        Console.WriteLine("Receiving message - " +
//                                          "Value1: {0}, Value2: {1}, Value3: {2}, Value4: {3}, Value5: {4}",
//                                          msg.Value1, msg.Value2, msg.Value3, msg.Value4, msg.Value5);
//
//                    });
//                    subs.Handler(del);
//                });
//            });

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

        class ClientDataConsumer : Consumes<ClientData>.Context
        {
            public void Consume(IConsumeContext<ClientData> message)
            {
                 Console.WriteLine("Receiving message - " +
                                   "Value1: {0}, Value2: {1}, Value3: {2}, Value4: {3}, Value5: {4}",
                                    message.Message.Value1, message.Message.Value2, message.Message.Value3,
                                    message.Message.Value4, message.Message.Value5);
         
                Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ")");
            }
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
