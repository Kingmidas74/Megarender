using System;
using System.Collections.Generic;

namespace Megarender.DataBus.Models
{
    public class SendSMSEvent: Event, IReasonable
    {
        public string Phone { get; set; }
        public string Reason { get; set; }
        
        public Dictionary<string,string> Variables { get; set; }
    }
}