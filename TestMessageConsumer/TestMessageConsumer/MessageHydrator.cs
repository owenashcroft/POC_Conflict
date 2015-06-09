using System.IO;
using System.Runtime.Serialization.Json;

namespace TestMessageConsumer
{
    public interface IMessageHydrator
    {
        T Hydrate<T>(byte[] rabbitMessageBody);
    }

    public class MessageHydrator : IMessageHydrator
    {
        public T Hydrate<T>(byte[] rabbitMessageBody)
        {
            var javaScriptSerializer = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream(rabbitMessageBody))
            {
                return (T) javaScriptSerializer.ReadObject(ms);
            }
        }
    }
}