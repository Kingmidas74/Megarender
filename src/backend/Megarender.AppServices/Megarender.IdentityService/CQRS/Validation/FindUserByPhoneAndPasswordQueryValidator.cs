using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class FindUserByPhoneAndPasswordQueryValidator : AbstractValidator<FindUserByPhoneAndPasswordQuery>
    {
        private readonly AppDbContext identityDBContext;
        private readonly UtilsService utils;
        private readonly IOptions<ApplicationOptions> options;
        public FindUserByPhoneAndPasswordQueryValidator(AppDbContext identityDBContext, UtilsService utils, IOptions<ApplicationOptions> options)
        {
            this.options = options;
            this.utils = utils;
            this.identityDBContext = identityDBContext;
            
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Phone).MustAsync((x,y,z)=>IsExist(x,z));
        }

        public async Task<bool> IsExist(FindUserByPhoneAndPasswordQuery query, CancellationToken cancellationToken = default)
        {
            var user = await identityDBContext.Users.SingleAsync(u => u.Phone == query.Phone, cancellationToken);
            return user.Password == utils.HashedPassword(user.Phone, query.Password, user.Salt, options.Value.Pepper);
        }
    }
}