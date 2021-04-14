using FluentValidation;
using Megarender.DataAccess;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        private IAPIContext DBContext {get;set;}
        public CreateUserCommandValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Birthdate).NotEmpty();
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.SecondName).NotEmpty();
            RuleFor(x=>x.SurName).NotEmpty();
            RuleFor(x => x.CommandId).NotEmpty();
        }
    }
}