using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.DataAccess;
using Megarender.Domain;
using Megarender.Features.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Features.Modules.OrganizationModule
{
    public class GetOrganizationQueryValidator:AbstractValidator<GetOrganizationQuery>
    {
        private readonly IAPIContext _apiContext;
        public GetOrganizationQueryValidator(IAPIContext apiContext) {
            _apiContext=apiContext;

            RuleFor(x=>x.Id).NotEmpty().MustAsync(IsExist);                            
        }

        private async Task<bool> IsExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return await _apiContext.Organizations.AnyAsync(
                    new FindByIdSpecification<Organization>(organizationId).ToExpression(),
                    cancellationToken);
        }
    }
}