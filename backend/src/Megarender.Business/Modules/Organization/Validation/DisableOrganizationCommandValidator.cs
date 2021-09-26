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
    public class DisableOrganizationCommandValidator:AbstractValidator<DisableOrganizationCommand>
    {
        private readonly IAPIContext _dbContext;
        public DisableOrganizationCommandValidator(IAPIContext dbContext) {
            _dbContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty().MustAsync(IsExist);
            RuleFor(x => x.ModifyBy).NotEmpty();
            RuleFor(x => x.CommandId).NotEmpty();
        }
        
        private Task<bool> IsExist(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Organizations.AnyAsync(new FindByIdSpecification<Organization>(organizationId).ToExpression(), cancellationToken);
        }
    }
}