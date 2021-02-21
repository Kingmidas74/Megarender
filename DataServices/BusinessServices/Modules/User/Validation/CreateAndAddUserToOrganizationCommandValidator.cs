using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class CreateAndAddUserToOrganizationCommandValidator:AbstractValidator<CreateAndAddUserToOrganizationCommand>
    {
        private IAPIContext DBContext {get;set;}
        public CreateAndAddUserToOrganizationCommandValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Birthdate).NotEmpty();
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.SecondName).NotEmpty();
            RuleFor(x=>x.SurName).NotEmpty();  
            RuleFor(x=>x.OrganizationId).NotEmpty().MustAsync(OrganizationExist);          
        }

        private async Task<bool> OrganizationExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return (await DBContext.Organizations.AnyAsync(x=>x.Id.Equals(organizationId), cancellationToken));
        }
    }
}