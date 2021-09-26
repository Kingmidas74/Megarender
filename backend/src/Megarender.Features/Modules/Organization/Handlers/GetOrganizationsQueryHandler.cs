using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;
using Megarender.Features.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Features.Modules.OrganizationModule
{
    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<Organization>>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrganizationsQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Organization>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Organizations.Where(new FindActiveSpecification<Organization>().ToExpression()).ToListAsync(cancellationToken);
        }
    }
}