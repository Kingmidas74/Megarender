using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class DisableOrganizationCommandHandler : IRequestHandler<DisableOrganizationCommand, Unit>
    {
        private readonly IAPIContext _dbContext;
        public DisableOrganizationCommandHandler(IAPIContext dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task<Unit> Handle(DisableOrganizationCommand request, CancellationToken cancellationToken = default)
        {
            var organization = (await _dbContext.Organizations.SingleAsync(new FindByIdSpecification<Organization>(request.Id).ToExpression(), cancellationToken));
            organization.Status = EntityStatusId.Inactive;
            var bindingsForDelete = await 
                _dbContext.UserOrganizations.Where(x => x.Organization.Id == request.Id).ToListAsync(cancellationToken);
            _dbContext.UserOrganizations.RemoveRange(bindingsForDelete);
            return Unit.Value;
        }
    }
}