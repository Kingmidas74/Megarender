using System;
using MediatR;

namespace Megarender.IdentityService.CQRS
{
    public class ConfirmIdentityCommand:IRequest<Guid>
    {
        public Guid Id {get;set;}
        public string Code {get;set;}
    }
}