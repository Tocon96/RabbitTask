using RabbitTask.Models;
using RabbitTask.Services;
using RabbitTask.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTask.Tests.UnitTests.Utils
{
    public class SenderTypeValidorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsValid_MessageProperType_ReturnsTrue()
        {
            ISenderTypeValidator validator = new SenderTypeValidator();

            EmailMessage message = new EmailMessage
            {
                Id = 0,
                From = "testfrom@mail.test",
                To = "testto@mail.test",
                Subject = "TestSubject",
                Body = "TestBody",
                Type = SenderTypeEnum.Smtp
            };

            bool result = validator.IsValid(message.Type);

            Assert.IsTrue(result);
        }
    }
}
