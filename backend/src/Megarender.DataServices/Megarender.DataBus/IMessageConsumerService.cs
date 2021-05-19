using System;
using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public interface IMessageConsumerService
    {
        void Subscribe(Func<object, bool> handler);
    }
}