using Newtonsoft.Json;
using System.Net.Mail;

namespace RabbitTask.Models
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<string>? CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
    }
}
