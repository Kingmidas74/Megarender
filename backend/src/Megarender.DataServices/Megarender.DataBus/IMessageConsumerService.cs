using System;
using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public interface IMessageConsumerService
    {
        void Subscribe<T>(Func<Envelope<T>,bool> handler) where T : IMessage;
    }
}