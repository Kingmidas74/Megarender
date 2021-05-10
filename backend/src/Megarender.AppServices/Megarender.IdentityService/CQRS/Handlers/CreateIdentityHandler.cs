using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class CreateIdentityHandler : IRequestHandler<CreateIdentityCommand, Guid>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly UtilsService _utils;
        private readonly ApplicationOptions _options;
        public CreateIdentityHandler(AppDbContext identityDbContext, IOptions<ApplicationOptions> options, UtilsService utils)        
        {
            _identityDbContext = identityDbContext;
            _utils = utils;
            _options = options?.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));
        }
        public async Task<Guid> Handle(CreateIdentityCommand request, CancellationToken cancellationToken = default)
        {
            Guid result;
            var code = _utils.GenerateCode(_options.LowerBoundCode, _options.UpperBoundCode);
            var identityExist = await _identityDbContext.Identities.FirstOrDefaultAsync(u=>u.Phone.Equals(request.Phone), cancellationToken);            
            if(identityExist!=null)
            {                
                identityExist.Code=code;                
                result = identityExist.Id;
            }
            else 
            {
                var salt = _utils.GenerateSalt(request.Password.Length);
                
                var identity = new Identity {
                    Id=request.Id,
                    Phone = request.Phone,
                    Salt = salt,
                    Password = _utils.HashedPassword(request.Phone,request.Password,salt,_options.Pepper),
                    Code = code
                };

                await _identityDbContext.Identities.AddAsync(identity, cancellationToken);

                result = identity.Id;
            }
            await _identityDbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}