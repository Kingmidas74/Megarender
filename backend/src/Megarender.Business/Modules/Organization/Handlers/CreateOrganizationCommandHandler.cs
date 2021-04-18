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
        private readonly IAPIContext _apiContext;
        private readonly IMapper _mapper;
        public CreateOrganizationCommandHandler(IAPIContext apiContext, IMapper mapper)
        {
            _apiContext=apiContext;
            _mapper = mapper;
        }
        public async Task<Organization> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken = default)
        {
            var organization = _mapper.Map<Organization>(request);
            organization.OrganizationUsers.Add(new UserOrganization()
            {
                Organization = organization,
                User = organization.CreatedBy
            });
            var createdOrganization = (await _apiContext.Organizations.AddAsync(_mapper.Map<Organization>(request),cancellationToken)).Entity;
            
            return createdOrganization;
        }
    }
}