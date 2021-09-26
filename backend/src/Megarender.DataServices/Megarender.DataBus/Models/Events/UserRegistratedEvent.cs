using System;

namespace Megarender.DataBus.Models
{
    public class UserRegistratedEvent:Event
    {
        public Guid UserId { get; set; }
    }
}