using System.Collections.Generic;

namespace Megarender.StorageService.Middleware
{
    public abstract class StorageResponse
    {
        public Dictionary<string,object> Body { get; internal set; } = new Dictionary<string, object>();
    }
}