using System;

namespace Megarender.DataBus
{
    public interface IMessageConsumerService
    {
        void Subscribe(Func<object, bool> handler);
    }
}