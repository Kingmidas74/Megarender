using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class GetOrganizationQuery:IRequest<Organization>
    {
        public Guid Id {get;set;}
    }
}