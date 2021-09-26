using System;
using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class SendCodeCommand:IRequest<Guid>
    {
        public string Phone { get; init; }
    }
}