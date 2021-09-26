using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Features.Modules.UserModule
{
    public class GetUserQuery:IRequest<User>
    {
        public Guid Id {get;set;}
    }
}