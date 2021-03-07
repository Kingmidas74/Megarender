using System.Collections.Generic;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class GetOrganizationsQuery:IRequest<IEnumerable<Organization>>
    {
    }
}