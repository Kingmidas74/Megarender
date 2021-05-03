using System;
using System.Collections.Generic;

namespace Megarender.ManagementService.Exceptions
{
    public class ServerException:Exception
    {
        public Type ExceptionType {get;set;}
        public Dictionary<string,object> Properties = new Dictionary<string,object>();
        public ServerException() { }
    }
}