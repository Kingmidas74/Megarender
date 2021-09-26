using System.Collections.Generic;
using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public class DefaultMessageProducerService: IMessageProducerService
    {
        public void Enqueue<T>(T message, Dictionary<string,string> headers) where T : IEvent
        {}
    }
}