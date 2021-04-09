using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.UserModule
{
    public class GetUsersByOrganizationQueryValidator:AbstractValidator<GetUsersByOrganizationQuery>
    {
        private IAPIContext DBContext {get;set;}
        public GetUsersByOrganizationQueryValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.OrganizationId).NotEmpty().MustAsync(isExist);                            
        }

        private async Task<bool> isExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return await DBContext.Organizations.AnyAsync(
                    new FindByIdSpecification<Organization>(organizationId).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}