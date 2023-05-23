using Newtonsoft.Json;
using RabbitTask.Models;

namespace RabbitTask.Utils
{
    public class MessageDeserializer : IMessageDeserializer
    {
        public EmailMessage DeserializeMessageJson(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            EmailMessage deserialized = serializer.Deserialize<EmailMessage>(new JsonTextReader(new StringReader(json)));
            return deserialized;
        }
    }
}
