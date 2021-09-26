using FluentValidation;

namespace Megarender.Business.Modules.Validators
{
    public class IdempotentRequestValidator:AbstractValidator<IIdempotentRequest>
    {
            public IdempotentRequestValidator() 
            {
                RuleFor(x=>x.CommandId).NotEmpty();            
            }
    }
}