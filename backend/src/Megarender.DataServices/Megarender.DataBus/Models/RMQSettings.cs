using System.Collections.Generic;
using Megarender.DataBus.Enums;

namespace Megarender.DataBus.Models
{
    public class ExchangeSettings
    {
        public string Name { get; set; }
        public AMQPExchanges Type { get; set; }
    }
    public class QueueSettings
    {
        public string Name { get; set; }
    }
    public class RMQSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
        
        public IEnumerable<ExchangeSettings> Exchanges { get; set; }

        public IEnumerable<QueueSettings> Queues { get; set; }
    }
}