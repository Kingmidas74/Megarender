using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService.CQRS
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly AppDbContext IdentityDBContext;

        public RemoveUserHandler(AppDbContext identityDBContext)
        {
            this.IdentityDBContext = identityDBContext;
        }
        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = await this.IdentityDBContext.Users.SingleAsync(x=>x.Phone.Equals(request.Phone), cancellationToken);
            this.IdentityDBContext.Remove(user);
            await this.IdentityDBContext.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}