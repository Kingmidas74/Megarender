using FluentValidation;

namespace Megarender.Features.Modules.Validators
{
    public class IdempotentRequestValidator:AbstractValidator<IIdempotentRequest>
    {
            public IdempotentRequestValidator() 
            {
                RuleFor(x=>x.CommandId).NotEmpty();            
            }
    }
}