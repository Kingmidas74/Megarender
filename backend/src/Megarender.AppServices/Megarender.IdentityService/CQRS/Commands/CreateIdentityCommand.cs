using System;
using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class CreateIdentityCommand:IRequest<Guid>
    {
        public Guid Id { get; init; }
        public string Password { get; init; }
        public string Phone { get; init; }
    }
}