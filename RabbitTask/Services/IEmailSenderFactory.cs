namespace RabbitTask.Services
{
    public interface IEmailSenderFactory
    {
        public IEmailSender GetEmailSender(string type, ILogger<IEmailSender> queueLogger);
    }
}
