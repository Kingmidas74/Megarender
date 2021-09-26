using System;

namespace Megarender.DataBus
{
    public class DefaultMessageConsumerService: IMessageConsumerService
    {
        public void Subscribe(Func<object, bool> handler)
        {}
    }
}