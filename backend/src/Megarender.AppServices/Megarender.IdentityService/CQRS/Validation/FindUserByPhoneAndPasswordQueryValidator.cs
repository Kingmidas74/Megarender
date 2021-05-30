using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class FindUserByPhoneAndPasswordQueryValidator : AbstractValidator<FindUserByPhoneAndPasswordQuery>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly UtilsService _utils;
        private readonly IOptions<ApplicationOptions> _options;
        public FindUserByPhoneAndPasswordQueryValidator(AppDbContext identityDbContext, UtilsService utils, IOptions<ApplicationOptions> options)
        {
            _options = options;
            _utils = utils;
            _identityDbContext = identityDbContext;
            
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Phone).MustAsync((x,y,z)=>IsExist(x,z));
        }

        private async Task<bool> IsExist(FindUserByPhoneAndPasswordQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _identityDbContext.Users.SingleAsync(u => u.CommunicationChannelsData.PhoneNumber == query.Phone, cancellationToken);
            return user.Password == _utils.HashedPassword(user.CommunicationChannelsData.PhoneNumber, user.Salt, _options.Value.Pepper);
        }
    }
}