using System.Collections.Generic;

namespace Megarender.IdentityService.Middleware
{
    public abstract class WebAPIResponse
    {
        public Dictionary<string,object> Body { get; internal set; } = new Dictionary<string, object>();
    }
}