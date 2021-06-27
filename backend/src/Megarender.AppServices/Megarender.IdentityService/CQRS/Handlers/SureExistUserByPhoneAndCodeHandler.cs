using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService.CQRS
{
    public class SureExistUserByPhoneAndCodeHandler : IRequestHandler<SureExistUserByPhoneAndCodeCommand, User>
    {
        private readonly AppDbContext _identityDbContext;

        public SureExistUserByPhoneAndCodeHandler(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task<User> Handle(SureExistUserByPhoneAndCodeCommand request, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Users.SingleAsync(x=>x.CommunicationChannelsData.PhoneNumber.Equals(request.Phone) && x.Password.Equals(request.Code),cancellationToken);
        }
    }
}