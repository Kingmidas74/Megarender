using System;
using System.Collections.Generic;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class GetUsersByOrganizationQuery:IRequest<IEnumerable<User>>
    {
        public Guid OrganizationId {get;set;}
    }
}