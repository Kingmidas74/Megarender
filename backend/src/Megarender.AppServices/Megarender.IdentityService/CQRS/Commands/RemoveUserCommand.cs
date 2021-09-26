using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class RemoveUserCommand:IRequest
    {
        public string Password { get; init; }
        public string Phone { get; init; }
    }
}