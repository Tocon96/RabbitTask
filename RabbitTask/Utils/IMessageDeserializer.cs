using RabbitTask.Models;

namespace RabbitTask.Utils
{
    public interface IMessageDeserializer
    {
        public EmailMessage DeserializeMessageJson(string json);
    }
}
