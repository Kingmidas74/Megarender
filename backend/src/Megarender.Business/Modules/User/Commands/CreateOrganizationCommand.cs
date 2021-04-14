using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateOrganizationCommand:IRequest<Organization>, IIdempotentRequest, ITransactionalRequest
    {
        public Guid Id {get;set;}
        public string UniqueIdentifier { get; set; }
        public Guid CommandId { get; set; }
    }
}