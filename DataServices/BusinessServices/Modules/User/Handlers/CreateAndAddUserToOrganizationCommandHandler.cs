using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.BusinessServices.Services;
using Megarender.BusinessServices.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.BusinessServices.Modules.UserModule
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
        public async Task<User> Handle(CreateAndAddUserToOrganizationCommand request, CancellationToken cancellationToken)
        {   
            var user = (await this.DBContext.Users.AddAsync(Mapper.Map<User>(request),cancellationToken)).Entity;
            var organization = await this.DBContext.Organizations.SingleAsync(
                    new FindByIdSpecification<Organization>(request.OrganizationId).IsSatisfiedByExpression);         
            organization.OrganizationUsers.Add(
                new UserOrganization {
                    User = user,
                    Organization = organization
                }
            );
            await this.DBContext.SaveChangesAsync();
            return user;
        }
    }
}