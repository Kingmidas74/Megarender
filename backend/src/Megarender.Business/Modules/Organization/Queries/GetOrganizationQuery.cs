using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class GetOrganizationQuery:IRequest<Organization>
    {
        public Guid Id {get;set;}
    }
}