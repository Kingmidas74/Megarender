using FluentValidation;

namespace Megarender.Business.Modules.PingModule
{
    public class CreatePingCommandValidator:AbstractValidator<CreatePingCommand>
    {
        public CreatePingCommandValidator() 
        {
            RuleFor(x=>x.Message).NotEqual("Error");
        }
    }
}