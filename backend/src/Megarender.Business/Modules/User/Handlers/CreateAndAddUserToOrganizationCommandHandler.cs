using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.Business.Services;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateAndAddUserToOrganizationCommandHandler : IRequestHandler<CreateAndAddUserToOrganizationCommand, User>
    {
        private IAPIContext DBContext;
        private IMapper Mapper;
        public CreateAndAddUserToOrganizationCommandHandler(IMapper mapper, IAPIContext dBContext)
        {
            Mapper = mapper;
            DBContext = dBContext;
        }
        public async Task<User> Handle(CreateAndAddUserToOrganizationCommand request, CancellationToken cancellationToken = default)
        {   
            var user = (await this.DBContext.Users.AddAsync(Mapper.Map<User>(request),cancellationToken)).Entity;
            var organization = await this.DBContext.Organizations.SingleAsync(
                    new FindByIdSpecification<Organization>(request.OrganizationId).IsSatisfiedByExpression, cancellationToken);         
            organization.OrganizationUsers.Add(
                new UserOrganization {
                    User = user,
                    Organization = organization
                }
            );
            await this.DBContext.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}