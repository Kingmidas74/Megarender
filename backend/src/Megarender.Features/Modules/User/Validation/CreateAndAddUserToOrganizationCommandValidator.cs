using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Features.Modules.UserModule
{
    public class CreateAndAddUserToOrganizationCommandValidator:AbstractValidator<CreateAndAddUserToOrganizationCommand>
    {
        private readonly IAPIContext _dbContext;
        public CreateAndAddUserToOrganizationCommandValidator(IAPIContext dbContext) {
            _dbContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Birthdate).NotEmpty();
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.SecondName).NotEmpty();
            RuleFor(x=>x.SurName).NotEmpty();  
            RuleFor(x=>x.OrganizationId).NotEmpty().MustAsync(OrganizationExist);    
            RuleFor(x => x.CommandId).NotEmpty();      
        }

        private async Task<bool> OrganizationExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return (await _dbContext.Organizations.AnyAsync(x=>x.Id.Equals(organizationId), cancellationToken));
        }
    }
}