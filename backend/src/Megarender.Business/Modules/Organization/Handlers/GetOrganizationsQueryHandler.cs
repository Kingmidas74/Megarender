using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<Organization>>
    {
        private readonly IAPIContext _apiContext;
        private readonly IMapper _mapper;
        public GetOrganizationsQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            _apiContext=dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Organization>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken = default)
        {
            return await _apiContext.Organizations.ToListAsync(cancellationToken);
        }
    }
}