using RabbitMQ.Client;
using RabbitTask.Controllers;

namespace RabbitTask.Services
{
    public interface IMessageQueueProducer
    {
        public uint SendQueueMessage<T>(T message, ILogger<MessageController> logger);
        public uint PurgeQueue(ILogger<MessageController> logger);
    }
}