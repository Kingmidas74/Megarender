using System;
using Megarender.DataBus.Models;

namespace Megarender.DataBus
{
    public class DefaultMessageConsumerService: IMessageConsumerService
    {
        public void Subscribe<T>(Func<Envelope<T>,bool> handler) where T : IMessage
        {}
    }
}