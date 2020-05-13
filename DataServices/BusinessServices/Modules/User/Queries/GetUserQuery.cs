using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class GetUserQuery:IRequest<User>
    {
        public Guid Id {get;set;}
    }
}