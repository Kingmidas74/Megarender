using System;
using System.Collections.Generic;

namespace Megarender.Business.Exceptions {
    public class BusinessException : Exception {

        public Dictionary<string,object> Properties = new Dictionary<string, object>();        
        public BusinessException (Type exceptionType) : base ($"{nameof(Megarender)}.{nameof(Business)}") {            
            Properties.Add(nameof(exceptionType),exceptionType);
        }
    }
}