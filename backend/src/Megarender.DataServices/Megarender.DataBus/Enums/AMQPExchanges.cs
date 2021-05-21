using System.ComponentModel;

namespace Megarender.DataBus.Enums
{
    public enum AMQPExchanges
    {
        [Description("amq.direct")]
        DIRECT,
        [Description("amq.headers")]
        HEADERS,
        [Description("amq.fanout")]
        FANOUT,
        [Description("amq.topic")]
        TOPIC
    }
}