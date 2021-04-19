using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Organization>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public CreateOrganizationCommandHandler(IAPIContext dbContext, IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
        }
        public async Task<Organization> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken = default)
        {            
            var organization = (await this._dbContext.Organizations.AddAsync(_mapper.Map<Organization>(request),cancellationToken)).Entity;
            var user = (await this._dbContext.Users.SingleAsync(new FindByIdSpecification<User>(request.CreatedBy).IsSatisfiedByExpression, cancellationToken));
            user.UserOrganizations.Add(new UserOrganization {
                Organization = organization,
                User = user
            });
            return organization;
        }
    }
}