using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class CreateOrganizationCommand:IRequest<Organization>, ITransactionalRequest, IIdempotentRequest
    {
        public Guid Id {get;set;}
        public string UniqueIdentifier { get; set; }
        public Guid CommandId { get; set; }
    }
}