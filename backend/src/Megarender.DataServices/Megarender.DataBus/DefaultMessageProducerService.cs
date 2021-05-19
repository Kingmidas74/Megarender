using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public class DefaultMessageProducerService: IMessageProducerService
    {
        public void Enqueue<T>(T message) where T : IEvent
        {}
    }
}