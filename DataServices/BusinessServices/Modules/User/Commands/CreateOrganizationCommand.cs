using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class CreateOrganizationCommand:IRequest<Organization>, ITransactionalRequest
    {
        public Guid Id {get;set;}
        public string UniqueIdentifier { get; set; }
    }
}