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
    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, Organization>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrganizationQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
        }
        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Organizations.SingleAsync(
                    new FindByIdSpecification<Organization>(request.Id).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}