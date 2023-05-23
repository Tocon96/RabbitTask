using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitTask.Controllers;
using RabbitTask.Models;
using RabbitTask.Utils;
using System.Text;

namespace RabbitTask.Services
{
    public class MessageQueueProducer : IMessageQueueProducer
    {
        private IModel channelModel;
        private ILogger logger;
        private string queueName = "mail";

        public MessageQueueProducer(ILogger logger, string queueName = "") 
        {
            this.logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channelModel = channel;

            if (!String.IsNullOrEmpty(queueName))
            {
                this.queueName = queueName;
            }
        }
        public uint SendQueueMessage(EmailMessage message)
        {
            try
            {
                channelModel.QueueDeclare(queueName, exclusive: false);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channelModel.BasicPublish(exchange: "", routingKey: queueName, body: body);
                return channelModel.MessageCount(queueName);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public void DeleteQueue()
        {
            try
            {
                channelModel.QueueDelete(queueName);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}
