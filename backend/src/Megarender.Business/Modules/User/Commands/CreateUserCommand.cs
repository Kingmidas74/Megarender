using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateUserCommand:IRequest<User>, IIdempotentRequest
    {
        public Guid Id {get; init;}
        public string FirstName {get; init;}
        public string SecondName {get; init;}
        public string SurName {get; init;}
        public DateTime Birthdate { get; init; }     
        public Guid CommandId { get; set; }
    }
}