using RabbitMQ.Client;
using RabbitTask.Controllers;
using RabbitTask.Models;

namespace RabbitTask.Services
{
    public interface IMessageQueueProducer
    {
        public uint SendQueueMessage(EmailMessage message);
        public void DeleteQueue();
    }
}