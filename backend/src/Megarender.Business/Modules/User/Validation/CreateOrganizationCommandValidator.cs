using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateOrganizationCommandValidator:AbstractValidator<CreateOrganizationCommand>
    {
        private IAPIContext DBContext {get;set;}
        public CreateOrganizationCommandValidator(IAPIContext dbContext) {
            DBContext=dbContext;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.UniqueIdentifier).NotEmpty()
                                            .MustAsync(isUnique);                            
        }

        private async Task<bool> isUnique(string organizationIdentifier, CancellationToken cancellationToken = default)
        {
            return !(await DBContext.Organizations.AnyAsync(x=>x.UniqueIdentifier.Equals(organizationIdentifier), cancellationToken));
        }
    }
}