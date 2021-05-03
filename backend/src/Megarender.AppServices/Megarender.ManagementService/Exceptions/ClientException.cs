using System.Collections.Generic;
using System;
namespace Megarender.ManagementService.Exceptions
{
    public class ClientException:Exception
    {
        public Type ExceptionType {get;set;}
        public Dictionary<string,object> Properties = new Dictionary<string,object>();
        public ClientException(Exception e):base(nameof(ClientException),e) { }
    }
}