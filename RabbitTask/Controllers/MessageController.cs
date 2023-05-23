using Microsoft.AspNetCore.Mvc;
using RabbitTask.Models;
using RabbitTask.Services;

namespace RabbitTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageQueueProducer queueProducer;
        private readonly ILogger<MessageController> messageControllerLogger;

        public MessageController(IMessageQueueProducer producer, ILogger<MessageController> logger)
        {
            queueProducer = producer;
            messageControllerLogger = logger;
        }
       
        [HttpPost("send")]
        public IActionResult Send(EmailMessage message)
        {
            try
            {
                queueProducer.SendQueueMessage(message);
                return Ok();
            }
            catch(Exception ex)
            {
                messageControllerLogger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}