using System;

namespace Megarender.DataBus.Models
{
    public class SendCodeMessage:Message, IMessage
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
    }
}