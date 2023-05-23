using Moq;
using RabbitTask.Models;
using RabbitTask.Services;
using RabbitTask.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTask.Tests.IntegrationTests.Services
{
    public class MessageQueueConsumerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Register_MessageOnQueue_MessageConsumedAndEmailSent()
        {
            var emailSenderMock = new Mock<IEmailSender>();
            ILogger logger = new Logger();
            var factoryMock = new Mock<IEmailSenderFactory>();
            factoryMock.Setup(emailSenderFactory => emailSenderFactory.GetEmailSender(It.IsAny<SenderTypeEnum>())).Returns(emailSenderMock.Object);
            IMessageDeserializer deserializer = new MessageDeserializer();
            IMessageQueueConsumer consumer = new MessageQueueConsumer(logger, factoryMock.Object, deserializer, "testmail");

            consumer.Register();

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

            emailSenderMock.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Exactly(1));

            producer.DeleteQueue();
        }
    }
}
