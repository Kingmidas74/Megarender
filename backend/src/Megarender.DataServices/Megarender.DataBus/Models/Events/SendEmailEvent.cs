using System;
using System.Collections.Generic;

namespace Megarender.DataBus.Models
{
    public class SendEmailEvent: Event, IReasonable
    {
        public string Email { get; set; }
        public string Reason { get; set; }
        
        public Dictionary<string,string> Variables { get; set; }
    }
}