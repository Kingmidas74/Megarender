using FluentValidation;

namespace Megarender.IdentityService.CQRS
{
    public class SureExistUserByPhoneAndCodeCommandValidator : AbstractValidator<SureExistUserByPhoneAndCodeCommand>
    {
        public SureExistUserByPhoneAndCodeCommandValidator()
        {
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}