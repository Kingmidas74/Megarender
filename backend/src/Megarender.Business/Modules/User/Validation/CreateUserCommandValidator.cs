using FluentValidation;
using Megarender.DataAccess;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IAPIContext dbContext) {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.CommandId).NotEmpty();
        }
    }
}