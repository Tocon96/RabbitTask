using RabbitTask.Models;

namespace RabbitTask.Utils
{
    public interface ISenderTypeValidator
    {
        bool IsValid(SenderTypeEnum type);
    }
}
