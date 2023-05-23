using RabbitTask.Models;

namespace RabbitTask.Services
{
    public class EmailSenderFactory : IEmailSenderFactory
    {
        private ILogger logger;

        public EmailSenderFactory(ILogger logger) 
        { 
            this.logger = logger;
        }

        public IEmailSender GetEmailSender(SenderTypeEnum type)
        {
            switch(type)
            {
                case SenderTypeEnum.Smtp:
                    return new SmtpSender(this.logger);
                default:
                    return new SmtpSender(this.logger);
            }
        }
    }
}
