using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Organization>
    {
        private IAPIContext DBContext;
        private IMapper Mapper;
        public CreateOrganizationCommandHandler(IAPIContext dbContext, IMapper mapper)
        {
            DBContext=dbContext;
            Mapper = mapper;
        }
        public async Task<Organization> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken = default)
        {
            var organization = (await this.DBContext.Organizations.AddAsync(Mapper.Map<Organization>(request),cancellationToken)).Entity;
            return organization;
        }
    }
}