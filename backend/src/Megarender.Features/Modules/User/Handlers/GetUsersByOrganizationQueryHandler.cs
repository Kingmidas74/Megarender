using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Features.Modules.UserModule
{
    public class GetUsersByOrganizationQueryHandler : IRequestHandler<GetUsersByOrganizationQuery, IEnumerable<User>>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public GetUsersByOrganizationQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> Handle(GetUsersByOrganizationQuery request, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.Where(s=>s.UserOrganizations
                                            .Select(x=>x.Organization)
                                            .Select(x=>x.Id)
                                            .Contains(request.OrganizationId))
                                        .ToListAsync(cancellationToken);
        }
    }
}