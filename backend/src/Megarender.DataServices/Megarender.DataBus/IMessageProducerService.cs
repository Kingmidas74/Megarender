using System.Collections.Generic;
using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public interface IMessageProducerService
    {
        void Enqueue<T>(T message, Dictionary<string,string> headers) where T:IEvent;
    }
}