using System;
using System.Collections.Generic;

namespace Megarender.Features.Exceptions {
    public class BusinessException : Exception {

        public Dictionary<string,object> Properties = new Dictionary<string, object>();        
        public BusinessException (Type exceptionType) : base ($"{nameof(Megarender)}.{nameof(Features)}") {            
            Properties.Add(nameof(exceptionType),exceptionType);
        }
    }
}