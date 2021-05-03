using System.Collections.Generic;

namespace Megarender.ManagementService.Middleware
{
    public abstract class WebAPIResponse
    {
        public Dictionary<string,object> Body { get; internal set; } = new Dictionary<string, object>();
    }
}