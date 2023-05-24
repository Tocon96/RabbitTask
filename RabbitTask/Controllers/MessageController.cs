using Microsoft.AspNetCore.Mvc;
using RabbitTask.Models;
using RabbitTask.Services;
using RabbitTask.Utils;
using System.ComponentModel.DataAnnotations;

namespace RabbitTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageQueueProducer queueProducer;
        private readonly Services.ILogger logger;
        private ISenderTypeValidator validator;

        public MessageController(IMessageQueueProducer producer, Services.ILogger logger, ISenderTypeValidator validator)
        {
            queueProducer = producer;
            this.logger = logger;
            this.validator = validator;
        }
       
        [HttpPost("send")]
        public IActionResult Send(EmailMessage message)
        {
            try
            {

                if (message == null || !validator.IsValid(message.Type))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var messageCount = queueProducer.SendQueueMessage(message);
                return Ok();
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}