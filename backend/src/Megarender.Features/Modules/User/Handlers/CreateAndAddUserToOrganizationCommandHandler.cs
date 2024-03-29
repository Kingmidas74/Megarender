using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;
using Megarender.Features.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Features.Modules.UserModule
{
    public class CreateAndAddUserToOrganizationCommandHandler : IRequestHandler<CreateAndAddUserToOrganizationCommand, User>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAndAddUserToOrganizationCommandHandler(IMapper mapper, IAPIContext dBContext)
        {
            _mapper = mapper;
            _dbContext = dBContext;
        }
        public async Task<User> Handle(CreateAndAddUserToOrganizationCommand request, CancellationToken cancellationToken = default)
        {   
            var user = (await _dbContext.Users.AddAsync(_mapper.Map<User>(request),cancellationToken)).Entity;
            var organization = await _dbContext.Organizations.SingleAsync(
                    new FindByIdSpecification<Organization>(request.OrganizationId).ToExpression(), cancellationToken);         
            organization.OrganizationUsers.Add(
                new UserOrganization {
                    User = user,
                    Organization = organization
                }
            );
            return user;
        }
    }
}