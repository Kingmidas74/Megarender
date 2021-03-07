using System.Collections.Generic;
using System;

namespace Megarender.Business.Exceptions {
    public class BusinessException : Exception {

        public Dictionary<string,object> Properties = new Dictionary<string, object>();        
        public BusinessException (Type exceptionType) : base ($"{nameof(Megarender)}.{nameof(Megarender.Business)}") {            
            Properties.Add(nameof(exceptionType),exceptionType);
        }
    }
}