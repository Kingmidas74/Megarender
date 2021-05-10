using System;
using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class ConfirmIdentityCommand:IRequest<Guid>
    {
        public Guid Id { get; init; }
        public string Code { get; init; }
    }
}