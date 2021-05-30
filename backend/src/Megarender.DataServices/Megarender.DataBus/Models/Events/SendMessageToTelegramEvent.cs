using System.Collections.Generic;

namespace Megarender.DataBus.Models
{
    public class SendMessageToTelegramEvent:Event, IReasonable
    {
        public string TelegramId { get; set; }
        public string Reason { get; set; }
        
        public Dictionary<string,string> Variables { get; set; }
    }
}