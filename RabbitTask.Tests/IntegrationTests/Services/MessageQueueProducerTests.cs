using Microsoft.Extensions.Logging;
using RabbitTask.Controllers;
using RabbitTask.Models;
using RabbitTask.Services;
using RabbitTask.Utils;
using System.Net.Mail;
using ILogger = RabbitTask.Services.ILogger;

namespace RabbitTask.Tests.IntegrationTests.Services
{
    public class MessageQueueProducerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSendMessage()
        {
            ILogger logger = new Logger();

            IMessageQueueProducer producer = new MessageQueueProducer(logger, "testmail");

            EmailMessage message = new EmailMessage
            {
                Id = 0,
                From = "testfrom@mail.test",
                To = "testto@mail.test",
                Subject = "TestSubject",
                Body = "TestBody",
                Type = SenderTypeEnum.Smtp
            };

            uint queueCountAfterSendingMessage = producer.SendQueueMessage(message);

            Assert.That(queueCountAfterSendingMessage, Is.EqualTo(1));

            producer.DeleteQueue();
        }
    }
}