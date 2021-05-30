using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class FindUserByPhoneAndPasswordQuery:IRequest<User>
    {
        public string Password {get;init;}
        public string Phone { get; init; }
    }
}