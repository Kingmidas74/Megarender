using System;

namespace Megarender.DataBus.Models
{
    public class SendCodeMessage:Message
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
    }
}