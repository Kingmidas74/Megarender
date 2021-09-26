using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class VerifyCodeCommand:IRequest<User>
    {
        public string Phone { get; init; }
        public string Code { get; init; }
        public string ServiceWebHook { get; init; }
    }
}