using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class SureExistUserByPhoneAndCodeCommand:IRequest<User>
    {
        public string Code {get;init;}
        public string Phone { get; init; }
    }
}