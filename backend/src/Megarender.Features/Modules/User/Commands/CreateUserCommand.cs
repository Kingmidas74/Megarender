using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Features.Modules.UserModule
{
    public class CreateUserCommand:IRequest<User>, IIdempotentRequest, ITransactionalRequest
    {
        public Guid Id {get; init;}  
        public Guid CommandId { get; set; }
    }
}