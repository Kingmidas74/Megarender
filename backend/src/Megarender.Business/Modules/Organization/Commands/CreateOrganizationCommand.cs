using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class CreateOrganizationCommand:IRequest<Organization>, IIdempotentRequest, ITransactionalRequest
    {
        public Guid Id {get; init;}
        public string UniqueIdentifier { get; init; }
        public Guid CommandId { get; init; }
        public Guid CreatedBy { get; set; }
    }
}