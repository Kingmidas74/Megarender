using System.Collections.Generic;

namespace Megarender.DataBus.Models
{
    public class Envelope<T>
    where T:IEvent
    {
        public Dictionary<string, string> Headers { get; set; } = new ();
        public T Message { get; set; }
    }
}