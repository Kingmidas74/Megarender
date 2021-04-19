using System;
using System.Linq;
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
        private readonly IAPIContext _dbContext;
        public CreateOrganizationCommandValidator(IAPIContext dbContext) {
            _dbContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.UniqueIdentifier).NotEmpty()
                                            .MustAsync(IsUnique);   
            RuleFor(x => x.CommandId).NotEmpty();  
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Organization should have owner")
                .MustAsync(IsExistAndHavePermissions).WithMessage("This user already own organization");
        }

        private async Task<bool> IsUnique(string organizationIdentifier, CancellationToken cancellationToken = default)
        {
            return !(await _dbContext.Organizations.AnyAsync(x=>x.UniqueIdentifier.Equals(organizationIdentifier), cancellationToken));
        }
        
        private async Task<bool> IsExistAndHavePermissions(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(new FindByIdSpecification<User>(userId).And(new FindActiveSpecification<User>()).ToExpression(), cancellationToken);
            var createdOrganizationsCount = await _dbContext.Organizations.CountAsync(x => x.CreatedBy.Id == userId, cancellationToken);
            return user is not null && createdOrganizationsCount == 0;
        }
    }
}