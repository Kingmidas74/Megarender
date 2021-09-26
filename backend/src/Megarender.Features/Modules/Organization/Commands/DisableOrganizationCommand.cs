using System;
using MediatR;

namespace Megarender.Features.Modules.OrganizationModule
{
    public class DisableOrganizationCommand:IRequest<Unit>, IIdempotentRequest, ITransactionalRequest
    {
        public Guid Id {get;set;}
        public Guid CommandId { get; set; }
        public Guid ModifyBy { get; set; }
    }
}