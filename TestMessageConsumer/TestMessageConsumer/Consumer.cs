using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestWCFProxy;

namespace TestMessageConsumer
{
    public class Consumer
    {
        private readonly string _queueName;
        private readonly int _interval;
        private readonly IDataSink _dataSink;
        private IMessageHydrator _messageHydrator;

        public Consumer(string queueName, int interval, IDataSink dataSink, IMessageHydrator messageHydrator)
        {
            _messageHydrator = messageHydrator;
            _queueName = queueName;
            _interval = interval;
            _dataSink = dataSink;
        }

        public void Check()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(_queueName, true, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(_queueName, true, consumer);

                    var nextCheck = DateTime.Now;

                    var iterations = 0;
                    while (true)
                    {
                        if (nextCheck < DateTime.Now)
                        {
                            GetQueuedMessages(consumer);

                            nextCheck = DateTime.Now.AddSeconds(_interval);
                            iterations = 0;
                        }
                        else
                        {
                            

                            if (iterations%1000 == 0)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Checking in {0} seconds          ", Math.Round(nextCheck.Subtract(DateTime.Now).TotalSeconds));
                            }
                        }
                        iterations++;
                    }
                }
            }
        }

        private void GetQueuedMessages(IQueueingBasicConsumer consumer)
        {
            Console.SetCursorPosition(0,1);

            Console.WriteLine("Polling message queue at {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);

            var hasMessage = true;
            var dequeued = 0;
            while (hasMessage)
            {
                BasicDeliverEventArgs ea;
                
                hasMessage = consumer.Queue.Dequeue(100, out ea);

                if (!hasMessage)
                {
                    TidyUpAfterDeQueuing(dequeued);
                    return;
                }

                dequeued++;

                var message = _messageHydrator.Hydrate<ClientData>(ea.Body);

                _dataSink.Save(message);

                if (dequeued%100 == 0)
                {
                    WriteDequeueStats(dequeued);
                }
            }
            
        }

        private static void TidyUpAfterDeQueuing(int dequeued)
        {
            if (dequeued > 0)
            {
                WriteDequeueStats(dequeued);
                Console.WriteLine();
            }
            Console.WriteLine("No more messages, going to sleep!");
        }

        private static void WriteDequeueStats(int dequeued)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("Dequeued {0} messages                 ", dequeued);
        }
    }
}