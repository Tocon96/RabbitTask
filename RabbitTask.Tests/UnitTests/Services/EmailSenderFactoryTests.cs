using Moq;
using RabbitTask.Models;
using RabbitTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTask.Tests.UnitTests.Services
{
    public class EmailSenderFactoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetEmailSender_SmtpType_Returns()
        {
            var loggerMock = new Mock<ILogger>();
            IEmailSenderFactory senderFactory = new EmailSenderFactory(loggerMock.Object);

            var sender = senderFactory.GetEmailSender(SenderTypeEnum.Smtp);

            Assert.IsInstanceOf<SmtpSender>(sender);
        }
    }
}
