using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using RabbitMQ.Client;
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
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("Queue1", true, false, false, null);
                    var javaScriptSerializer = new JavaScriptSerializer();

                    var serializedClientData = clientData.Select(clientInfo => javaScriptSerializer.Serialize(clientInfo));
                    foreach (var message in serializedClientData)
                    {
                        channel.BasicPublish("", "Queue1", null, Encoding.UTF8.GetBytes(message));
                    }
                }
            }

            return true;
        }
    }
}
