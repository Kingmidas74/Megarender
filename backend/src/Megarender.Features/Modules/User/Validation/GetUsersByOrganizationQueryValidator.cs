using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.DataAccess;
using Megarender.Domain;
using Megarender.Features.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Features.Modules.UserModule
{
    public class GetUsersByOrganizationQueryValidator:AbstractValidator<GetUsersByOrganizationQuery>
    {
        private readonly IAPIContext _dbContext;
        public GetUsersByOrganizationQueryValidator(IAPIContext dbContext) {
            _dbContext=dbContext;

            RuleFor(x=>x.OrganizationId).NotEmpty().MustAsync(IsExist);                            
        }

        private async Task<bool> IsExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Organizations.AnyAsync(
                    new FindByIdSpecification<Organization>(organizationId).ToExpression(),
                    cancellationToken);
        }
    }
}