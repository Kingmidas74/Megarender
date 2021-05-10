using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class ConfirmIdentityCommandValidator:AbstractValidator<ConfirmIdentityCommand>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly ApplicationOptions _options;
        public ConfirmIdentityCommandValidator(AppDbContext identityDbContext, IOptions<ApplicationOptions> options)
        {
            _identityDbContext = identityDbContext;
            _options = options.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));

            RuleFor(x=>x.Id).NotEmpty().MustAsync(IdentityExist);
            RuleFor(x=>x.Code).NotEmpty().Must(IsWithin);            
        }

        private async Task<bool> IdentityExist(Guid id, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Identities.AnyAsync(x=>x.Id.Equals(id), cancellationToken);
        }

        private bool IsWithin(string code)
        {
            return _options.LowerBoundCode<=int.Parse(code) && int.Parse(code)<=_options.UpperBoundCode;
        }
    }
}