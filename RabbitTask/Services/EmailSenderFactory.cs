namespace RabbitTask.Services
{
    public class EmailSenderFactory : IEmailSenderFactory
    {
        public IEmailSender GetEmailSender(string type, ILogger<IEmailSender> queueLogger)
        {
            switch(type)
            {
                case "smtp":
                    return new SmtpSender(queueLogger);
                default:
                    return new SmtpSender(queueLogger);
            }
        }
    }
}
