using RabbitTask.Models;

namespace RabbitTask.Utils
{
    public class SenderTypeValidator : ISenderTypeValidator
    {
        public bool IsValid(SenderTypeEnum type)
        {
            if(type == SenderTypeEnum.Smtp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
