using System;
using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public class DefaultMessageConsumerService: IMessageConsumerService
    {
        public void Subscribe(Func<object, bool> handler)
        {}
    }
}