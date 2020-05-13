using System.Collections.Generic;
using MediatR;
using Megarender.Domain;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class GetOrganizationsQuery:IRequest<IEnumerable<Organization>>
    {
    }
}