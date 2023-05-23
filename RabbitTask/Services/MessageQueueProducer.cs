using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitTask.Controllers;
using System.Text;

namespace RabbitTask.Services
{
    public class MessageQueueProducer : IMessageQueueProducer
    {
        private IModel channelModel { get; set; }
        private IConnection connectionRabbit { get; set; }

        public MessageQueueProducer() 
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channelModel = channel;
            connectionRabbit = connection;
        
        }
        public uint SendQueueMessage<T>(T message, ILogger<MessageController> logger)
        {
            try
            {
                channelModel.QueueDeclare("mail", exclusive: false);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channelModel.BasicPublish(exchange: "", routingKey: "mail", body: body);
                return channelModel.MessageCount("mail");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public uint PurgeQueue(ILogger<MessageController> logger)
        {
            try
            {
                channelModel.QueuePurge("mail");
                return channelModel.MessageCount("mail");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
