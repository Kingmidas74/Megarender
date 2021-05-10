using System.Text.RegularExpressions;
using FluentValidation;

namespace Megarender.IdentityService.CQRS
{
    public class SendCodeCommandValidator:AbstractValidator<SendCodeCommand>
    {
        public SendCodeCommandValidator(AppDbContext identityDbContext)
        {
            RuleFor(x=>x.Phone).NotEmpty().Must(x=>Regex.IsMatch(x,@"^\d*\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$"));            
        }
    }
}