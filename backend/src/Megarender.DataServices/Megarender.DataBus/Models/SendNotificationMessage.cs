using System;

namespace Megarender.DataBus.Models
{
    public class SendNotificationMessage:Message
    {
        public Guid UserId { get; set; }
    }
}