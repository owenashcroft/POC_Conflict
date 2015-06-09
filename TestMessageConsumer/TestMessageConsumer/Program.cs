using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMessageConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueName = ConfigurationManager.AppSettings["Queue"];
            var interval = int.Parse(ConfigurationManager.AppSettings["Interval"]);

            var sqlDataSink = new SqlDataSink(ConfigurationManager.ConnectionStrings["database"]);

            var consumer = new Consumer(queueName, interval, sqlDataSink, new MessageHydrator());

            Console.WriteLine("Monitoring queue {0} every {1} seconds", queueName, interval);

            consumer.Check();
        }
    }
}
