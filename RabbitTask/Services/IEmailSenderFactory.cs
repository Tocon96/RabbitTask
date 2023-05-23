using RabbitTask.Models;

namespace RabbitTask.Services
{
    public interface IEmailSenderFactory
    {
        public IEmailSender GetEmailSender(SenderTypeEnum type);
    }
}
