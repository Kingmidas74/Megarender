using System.Collections.Generic;
using Megarender.DataBus.Enums;

namespace Megarender.DataBus.Models
{
    public class ExchangeSettings
    {
        public string Name { get; set; }
        public AMQPExchanges Type { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
    }
    public class QueueSettings
    {
        public string Name { get; set; }
        public bool Durable { get; set; }
        public int PrefetchCount { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
    }
    public class RMQSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
        
        public IEnumerable<ExchangeSettings> Exchanges { get; set; }

        public IEnumerable<QueueSettings> Queues { get; set; }
    }
}