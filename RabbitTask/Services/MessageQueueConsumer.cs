using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitTask.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RabbitTask.Services
{
    public class MessageQueueConsumer : IMessageQueueConsumer
    {
        private readonly ILogger<MessageQueueConsumer> consumerLogger;
        private readonly ILogger<IEmailSender> senderLogger;

        public MessageQueueConsumer(ILogger<MessageQueueConsumer> logger)
        {
            consumerLogger = logger;
            LoggerFactory factory = new LoggerFactory();
            senderLogger = factory.CreateLogger<IEmailSender>();
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
                channel.QueueDeclare("mail", exclusive: false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, eventArgs) => {

                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    EmailMessage emailMessage = DeserializeMessageJson(message);
                    EmailSenderFactory emailSenderFactory = new EmailSenderFactory();

                    IEmailSender sender = emailSenderFactory.GetEmailSender(emailMessage.Type, senderLogger);

                    sender.Send(emailMessage);
                };

                channel.BasicConsume(queue: "mail", autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                consumerLogger.LogError(ex.Message);
                throw;
            }
        }

        private EmailMessage DeserializeMessageJson(string json)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                EmailMessage deserialized = serializer.Deserialize<EmailMessage>(new JsonTextReader(new StringReader(json)));
                return deserialized;
            }
            catch(Exception ex) 
            {
                consumerLogger.LogError(ex.Message);
                throw;
            }
        }

       
    }
}