using Megarender.DataBus.Enums;

namespace Megarender.DataBus.Models
{
    public class ExchangeSettings
    {
        public string Name { get; set; }
        public AMQPExchanges Type { get; set; }
    }
    public class RMQSettings
    {
        public bool Enabled { get; set; }
        public string ConntectionString { get; set; }
        
        public ExchangeSettings Exchange { get; set; }
    }
}