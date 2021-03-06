using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class GetOrganizationQuery:IRequest<Organization>
    {
        public Guid Id {get;set;}
    }
}