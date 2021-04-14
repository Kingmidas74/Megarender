using System;

namespace Megarender.DataBus
{
    public class DefaultMessageProducerService: IMessageProducerService
    {
        public void Enqueue(string messageString, string routingKey)
        {
            throw new NotImplementedException();
        }
    }
}