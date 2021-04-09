using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class GetUserQuery:IRequest<User>
    {
        public Guid Id {get;set;}
    }
}