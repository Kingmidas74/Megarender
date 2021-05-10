using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService.CQRS
{
    public class CreateIdentityCommandValidator:AbstractValidator<CreateIdentityCommand>
    {
        private readonly AppDbContext _identityDbContext;
        public CreateIdentityCommandValidator(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty().Must(x=>Regex.IsMatch(x,@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"));
            RuleFor(x=>x.Phone).NotEmpty().Must(x=>Regex.IsMatch(x,@"^\d*\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$")).MustAsync(BeUniquePhone);            
        }

        private async Task<bool> BeUniquePhone(string phone, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Users
                .AllAsync(l => l.Phone != phone, cancellationToken);
        }
    }
}