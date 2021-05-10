using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class VerifyCodeCommandValidator:AbstractValidator<VerifyCodeCommand>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly ApplicationOptions _options;
        public VerifyCodeCommandValidator(AppDbContext identityDbContext, IOptions<ApplicationOptions> options)
        {
            _identityDbContext = identityDbContext;
            _options = options.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));

            RuleFor(x=>x.Id).NotEmpty().MustAsync(AcceptableIdentifier);
            RuleFor(x=>x.Code).NotEmpty().Must(IsWithin);            
        }

        private async Task<bool> AcceptableIdentifier(Guid id, CancellationToken cancellationToken = default)
        {
            return (await _identityDbContext.Identities.AnyAsync(x=>x.Id.Equals(id), cancellationToken))
                || (await _identityDbContext.Users.AnyAsync(x=>x.Id.Equals(id), cancellationToken));
        }

        private bool IsWithin(string code)
        {
            return _options.LowerBoundCode<=int.Parse(code) && int.Parse(code)<=_options.UpperBoundCode;
        }
    }
}