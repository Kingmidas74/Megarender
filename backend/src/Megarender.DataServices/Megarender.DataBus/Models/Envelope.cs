using System.Collections.Generic;

namespace Megarender.DataBus.Models
{
    public class Envelope<T>
    where T:IMessage
    {
        public Dictionary<string, string> Headers { get; set; } = new ();
        public T Message { get; set; }
    }
}