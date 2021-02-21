using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.BusinessServices.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class GetOrganizationQueryValidator:AbstractValidator<GetOrganizationQuery>
    {
        private IAPIContext DBContext {get;set;}
        public GetOrganizationQueryValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty().MustAsync(isExist);                            
        }

        private async Task<bool> isExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return await DBContext.Organizations.AnyAsync(
                    new FindByIdSpecification<Organization>(organizationId).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}