using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService.CQRS
{
    public class ConfirmIdentityHandler : IRequestHandler<ConfirmIdentityCommand, Guid>
    {
        private readonly AppDbContext IdentityDBContext;
        public ConfirmIdentityHandler(AppDbContext identityDBContext)
        {
            this.IdentityDBContext = identityDBContext;
        }

        public async Task<Guid> Handle(ConfirmIdentityCommand request, CancellationToken cancellationToken = default)
        {
            var identity = await IdentityDBContext.Identities.SingleAsync(x=>x.Code.Equals(request.Code) && x.Id.Equals(request.Id), cancellationToken);
            
            await IdentityDBContext.Users.AddAsync(new User {
                Id=identity.Id,
                Password=identity.Password,
                Phone=identity.Phone,
                Salt=identity.Salt                        
            }, cancellationToken);

            IdentityDBContext.Identities.Remove(identity);
            await IdentityDBContext.SaveChangesAsync(cancellationToken);
            return request.Id;            
        }
    }
}