using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class CreateOrganizationCommand:IRequest<Organization>, IIdempotentRequest, ITransactionalRequest
    {
        public Guid Id {get;set;}
        public string UniqueIdentifier { get; set; }
        public Guid CommandId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}