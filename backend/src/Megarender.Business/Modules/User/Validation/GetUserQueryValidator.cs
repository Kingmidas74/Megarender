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
    public class GetUserQueryValidator:AbstractValidator<GetUserQuery>
    {
        private readonly IAPIContext _dbContext;
        public GetUserQueryValidator(IAPIContext dbContext) {
            _dbContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty().MustAsync(IsExist);                            
        }

        private async Task<bool> IsExist(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.AnyAsync(
                    new FindByIdSpecification<User>(userId).ToExpression(),
                    cancellationToken);
        }
    }
}