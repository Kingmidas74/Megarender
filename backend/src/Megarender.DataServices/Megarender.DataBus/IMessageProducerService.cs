using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public interface IMessageProducerService
    {
        void Enqueue<T>(T message) where T:IEvent;
    }
}