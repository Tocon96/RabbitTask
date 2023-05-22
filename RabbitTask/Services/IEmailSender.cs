using RabbitTask.Models;

namespace RabbitTask.Services
{
    public interface IEmailSender
    {
        bool Send(EmailMessage message);
    }
}
