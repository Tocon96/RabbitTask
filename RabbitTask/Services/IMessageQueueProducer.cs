using RabbitMQ.Client;
using RabbitTask.Controllers;

namespace RabbitTask.Services
{
    public interface IMessageQueueProducer
    {
        public uint SendQueueMessage<T>(T message);
        public uint PurgeQueue();
    }
}