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
    public class GetUserQueryValidator:AbstractValidator<GetUserQuery>
    {
        private IAPIContext DBContext {get;set;}
        public GetUserQueryValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty().MustAsync(isExist);                            
        }

        private async Task<bool> isExist(Guid userId, CancellationToken cancellationToken = default)
        {
            return await DBContext.Users.AnyAsync(
                    new FindByIdSpecification<User>(userId).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}