using System.Collections.Generic;
using MediatR;
using Megarender.Domain;

namespace Megarender.Features.Modules.OrganizationModule
{
    public class GetOrganizationsQuery:IRequest<IEnumerable<Organization>>
    {
    }
}