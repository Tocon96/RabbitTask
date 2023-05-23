using Microsoft.Extensions.Logging;
using RabbitTask.Controllers;
using RabbitTask.Models;
using RabbitTask.Services;
using System.Net.Mail;

namespace RabbitTask.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSendMessage()
        {
            MessageQueueProducer producer = new MessageQueueProducer();

            EmailMessage message = new EmailMessage
            {
                Id = 0,
                From = "testfrom@mail.test",
                To = "testto@mail.test",
                Subject = "TestSubject",
                Body = "TestBody"
            };

            uint queueCountAfterSendingMessage = producer.SendQueueMessage(message);

            Assert.That(queueCountAfterSendingMessage, Is.EqualTo(1));

            uint queueCountAfterPurge = producer.PurgeQueue();

            Assert.That(queueCountAfterPurge, Is.EqualTo(0));
        }
    }
}