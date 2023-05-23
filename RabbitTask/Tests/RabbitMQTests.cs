using NUnit.Framework;
using RabbitMQ.Client;
using RabbitTask.Controllers;
using RabbitTask.Models;
using RabbitTask.Services;

namespace RabbitTask.Tests
{
    [TestFixture]
    public class RabbitMQTests
    {
        [Test] 
        public void TestSendMessage()
        {

            LoggerFactory factory = new LoggerFactory();
            ILogger<MessageController> controllerLogger = factory.CreateLogger<MessageController>();

            MessageQueueProducer producer = new MessageQueueProducer();

            EmailMessage message = new EmailMessage
            {
                Id = 0,
                From = "karol.kula2@gmail.com",
                To = "bulltocon@gmail.com",
                Subject = "TestSubject",
                Body = "TestBody"
            };

            uint queueCountAfterSendingMessage = producer.SendQueueMessage(message, controllerLogger);

            Assert.AreEqual(1, queueCountAfterSendingMessage);

            uint queueCountAfterPurge = producer.PurgeQueue(controllerLogger);

            Assert.AreEqual(0, queueCountAfterPurge);
        }
    }
}
