using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService.CQRS
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly AppDbContext _identityDbContext;

        public RemoveUserHandler(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _identityDbContext.Users.SingleAsync(x=>x.CommunicationChannelsData.PhoneNumber.Equals(request.Phone), cancellationToken);
            _identityDbContext.Remove(user);
            await _identityDbContext.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}