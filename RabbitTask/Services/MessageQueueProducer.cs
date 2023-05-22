using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitTask.Controllers;
using System.Text;

namespace RabbitTask.Services
{
    public class MessageQueueProducer : IMessageQueueProducer
    {
        public IModel Channel { get; set; }
        public IConnection Connection { get; set; }

        public MessageQueueProducer() 
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            Channel = channel;
            Connection = connection;
        
        }
        public uint SendQueueMessage<T>(T message, ILogger<MessageController> logger)
        {
            try
            {
                Channel.QueueDeclare("mail", exclusive: false);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                Channel.BasicPublish(exchange: "", routingKey: "mail", body: body);
                return Channel.MessageCount("mail");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public uint PurgeQueue(IModel channel, ILogger<MessageController> logger)
        {
            try
            {
                channel.QueuePurge("mail");
                return channel.MessageCount("mail");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
