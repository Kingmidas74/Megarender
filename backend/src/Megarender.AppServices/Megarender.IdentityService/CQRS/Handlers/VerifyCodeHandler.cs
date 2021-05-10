using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, string>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly UtilsService _utils;
        private readonly ApplicationOptions _options;
        public VerifyCodeHandler(AppDbContext identityDbContext, UtilsService utils, IOptions<ApplicationOptions> options)
        {
            _identityDbContext = identityDbContext;
            _utils = utils;
            _options = options?.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));
        }

        public async Task<string> Handle(VerifyCodeCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _identityDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
            var identity = await _identityDbContext.Identities.FirstOrDefaultAsync(x=>x.Code.Equals(request.Code) && x.Id.Equals(request.Id), cancellationToken);


            if (user != null && identity != null)
            {
                _identityDbContext.Identities.Remove(identity);
                var salt = _utils.GenerateSalt(identity.Phone.Length);
                user.Password = _utils.HashedPassword(user.Phone, request.Code, salt, _options.Pepper);
                await _identityDbContext.SaveChangesAsync(cancellationToken);
                return user.Password;
            }
            if (user == null && identity != null)
            {
                var salt = _utils.GenerateSalt(identity.Phone.Length);
                var newUser = new User
                {
                    Id = identity.Id,
                    Password = _utils.HashedPassword(identity.Phone, request.Code, salt, _options.Pepper),
                    Phone = identity.Phone,
                    Salt = salt
                };
                await _identityDbContext.Users.AddAsync(newUser, cancellationToken);
                _identityDbContext.Identities.Remove(identity);    
                await _identityDbContext.SaveChangesAsync(cancellationToken);
                return newUser.Password;
            }

            throw new ConstraintException();
        }
    }
}