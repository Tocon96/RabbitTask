using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitTask.Models;
using RabbitTask.Utils;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RabbitTask.Services
{
    public class MessageQueueConsumer : IMessageQueueConsumer
    {
        private ILogger logger;
        private IEmailSenderFactory emailSenderFactory;
        private IMessageDeserializer deserializer;
        private string queueName = "mail";

        public MessageQueueConsumer(ILogger logger, IEmailSenderFactory emailSenderFactory, IMessageDeserializer deserializer, string queueName = "")
        {
            this.logger = logger;
            this.emailSenderFactory = emailSenderFactory;
            this.deserializer = deserializer;
            if(!String.IsNullOrEmpty(queueName)) 
            {
                this.queueName = queueName;
            }
        }

        public void Register()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost"
                };

                var connection = factory.CreateConnection();

                var channel = connection.CreateModel();
                channel.QueueDeclare(queueName, exclusive: false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, eventArgs) => {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    EmailMessage emailMessage = deserializer.DeserializeMessageJson(message);
                    IEmailSender sender = emailSenderFactory.GetEmailSender(emailMessage.Type);

                    sender.Send(emailMessage);
                    
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}