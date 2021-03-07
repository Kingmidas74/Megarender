using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateUserCommand:IRequest<User>, ITransactionalRequest, IIdempotentRequest
    {
        public Guid Id {get;set;}
        public string FirstName {get; set;}
        public string SecondName {get; set;}
        public string SurName {get; set;}
        public DateTime Birthdate { get; set; }     
        public Guid CommandId { get; set; }
    }
}