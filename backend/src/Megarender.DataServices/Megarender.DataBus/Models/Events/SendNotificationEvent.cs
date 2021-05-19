using System;

namespace Megarender.DataBus.Models
{
    public class SendNotificationEvent:Event
    {
        public Guid UserId { get; set; }
    }
}