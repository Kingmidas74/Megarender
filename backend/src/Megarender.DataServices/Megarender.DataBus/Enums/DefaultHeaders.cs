using System.ComponentModel;

namespace Megarender.DataBus.Enums
{
    public enum DefaultHeaders
    {
        [Description("x-event-type")]
        EventType=0,
        [Description("x-event-parent")]
        Parent=1
    }
}