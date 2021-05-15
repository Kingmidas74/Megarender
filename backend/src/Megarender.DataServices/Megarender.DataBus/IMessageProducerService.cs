using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public interface IMessageProducerService
    {
        void Enqueue<T>(Envelope<T> message, string routingKey) where T:IMessage;
    }
}