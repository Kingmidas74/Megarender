using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;

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
            return organization;
        }
    }
}