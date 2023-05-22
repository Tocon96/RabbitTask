using Newtonsoft.Json;
using RabbitTask.Models;
using System.Net;
using System.Net.Mail;

namespace RabbitTask.Services
{

    public class SmtpSender : IEmailSender
    {
        private ILogger<IEmailSender> senderLogger { get; set; }
        public SmtpSender(ILogger<IEmailSender> logger) {
            senderLogger = logger;
        }
        public bool Send(EmailMessage message)
        {
            try
            {
                SmtpDetails details = GetSmtpConfigurationDetails();
                SmtpClient smtpClient = ConfigureSmtpClient(details);

                MailMessage mailMessage = PrepareMailMessage(message);
                smtpClient.Send(mailMessage);
                return true;

            }
            catch (Exception ex)
            {
                senderLogger.LogError(ex.Message);
                return false;
            }


        }

        private SmtpDetails GetSmtpConfigurationDetails()
        {
            try
            {
                var reader = new StreamReader(@"./smtpconfiguration.json");
                SmtpDetails details = new SmtpDetails();
                string jsonFromFile = reader.ReadToEnd();
                if (!String.IsNullOrEmpty(jsonFromFile))
                {
                    details = JsonConvert.DeserializeObject<SmtpDetails>(jsonFromFile);
                }
                return details;
            }
            catch (Exception ex)
            {
                senderLogger.LogError(ex.Message);
                throw;
            }
        }

        private SmtpClient ConfigureSmtpClient(SmtpDetails details)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Host = details.Host;
                smtpClient.EnableSsl = details.Ssl;

                smtpClient.Credentials = new NetworkCredential(details.Username, details.Password);
                smtpClient.Port = details.Port;
                return smtpClient;
            }
            catch (Exception ex)
            {
                senderLogger.LogError(ex.Message);
                throw;
            }
        }

        private MailMessage PrepareMailMessage(EmailMessage message)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(new MailAddress(message.From), new MailAddress(message.To));
                mailMessage.Subject = message.Subject;
                mailMessage.Body = message.Body;

                return mailMessage;
            }
            catch (Exception ex)
            {
                senderLogger.LogError(ex.Message);
                throw;
            }
        }
    }
}
