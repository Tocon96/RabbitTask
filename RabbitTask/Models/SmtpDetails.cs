namespace RabbitTask.Models
{
    public class SmtpDetails
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
    }
}
