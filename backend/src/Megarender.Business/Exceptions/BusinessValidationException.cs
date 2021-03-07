using System.Linq;
using FluentValidation;

namespace Megarender.Business.Exceptions
{
    public class BusinessValidationException:BusinessException
    {
        private ValidationException validationException;

        public BusinessValidationException(ValidationException validationException):base(typeof(ValidationException)) {
            this.validationException = validationException;
            Properties = validationException.Errors.ToDictionary(x=>x.PropertyName, x=>(object)x.ErrorMessage);
        }
    }
}