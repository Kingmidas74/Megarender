using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public class DefaultMessageProducerService: IMessageProducerService
    {
        public void Enqueue<T>(Envelope<T> envelope, string routingKey) where T : IMessage
        {}
    }
}