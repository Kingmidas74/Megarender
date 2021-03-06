using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.BusinessServices.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class GetUsersByOrganizationQueryHandler : IRequestHandler<GetUsersByOrganizationQuery, IEnumerable<User>>
    {
        private IAPIContext DBContext;
        private IMapper Mapper;
        public GetUsersByOrganizationQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            DBContext=dbContext;
            Mapper = mapper;
        }
        public async Task<IEnumerable<User>> Handle(GetUsersByOrganizationQuery request, CancellationToken cancellationToken = default)
        {
            return await DBContext.Users.Where(s=>s.UserOrganizations
                                            .Select(x=>x.Organization)
                                            .Select(x=>x.Id)
                                            .Contains(request.OrganizationId))
                                        .ToListAsync(cancellationToken);
        }
    }
}