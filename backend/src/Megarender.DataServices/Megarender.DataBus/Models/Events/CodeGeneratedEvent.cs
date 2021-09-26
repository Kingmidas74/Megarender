using System;

namespace Megarender.DataBus.Models
{
    public class CodeGeneratedEvent:Event
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
    }
}