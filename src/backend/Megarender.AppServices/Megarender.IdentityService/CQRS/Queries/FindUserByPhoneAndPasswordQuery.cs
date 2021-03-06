using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class FindUserByPhoneAndPasswordQuery:IRequest<User>
    {
        public string Phone {get;set;}
        public string Password {get;set;}
    }
}