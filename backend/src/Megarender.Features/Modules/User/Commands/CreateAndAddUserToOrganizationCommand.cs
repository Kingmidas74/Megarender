using System;
using MediatR;
using Megarender.Domain;

namespace Megarender.Features.Modules.UserModule
{
    public class CreateAndAddUserToOrganizationCommand:IRequest<User>, IIdempotentRequest, ITransactionalRequest
    {
        public Guid Id {get;set;}
        public string FirstName {get; set;}
        public string SecondName {get; set;}
        public string SurName {get; set;}
        public DateTime Birthdate {get;set;}
        public Guid OrganizationId {get;set;}
        public Guid CommandId { get; set; }
    }
}