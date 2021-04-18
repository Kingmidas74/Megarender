using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class CreateOrganizationCommandValidator:AbstractValidator<CreateOrganizationCommand>
    {
        private IAPIContext DBContext {get;set;}
        public CreateOrganizationCommandValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.UniqueIdentifier).NotEmpty()
                                            .MustAsync(isUnique);   
            RuleFor(x => x.CommandId).NotEmpty();  
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Organization should have owner")
                .MustAsync(IsExistAndHavePermissions).WithMessage("This user already own organization");
        }

        private async Task<bool> isUnique(string organizationIdentifier, CancellationToken cancellationToken = default)
        {
            return !(await DBContext.Organizations.AnyAsync(x=>x.UniqueIdentifier.Equals(organizationIdentifier), cancellationToken));
        }
        
        private async Task<bool> IsExistAndHavePermissions(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await DBContext.Users.SingleOrDefaultAsync(new FindByIdSpecification<User>(userId).IsSatisfiedByExpression, cancellationToken);
            var createdOrganizationsCount = await DBContext.Organizations.CountAsync(x => x.CreatedBy.Id == userId, cancellationToken);
            return user is not null && createdOrganizationsCount == 0;
        }
    }
}