using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService.CQRS
{
    public class FindUserByPhoneAndPasswordHandler : IRequestHandler<FindUserByPhoneAndPasswordQuery, User>
    {
        private readonly AppDbContext _identityDbContext;

        public FindUserByPhoneAndPasswordHandler(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task<User> Handle(FindUserByPhoneAndPasswordQuery request, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Users.SingleAsync(x=>x.CommunicationChannelsData.PhoneNumber.Equals(request.Phone) && x.Password.Equals(request.Password),cancellationToken);
        }
    }
}