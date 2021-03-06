using System.Collections.Generic;
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
    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<Organization>>
    {
        private IAPIContext DBContext;
        private IMapper Mapper;
        public GetOrganizationsQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            DBContext=dbContext;
            Mapper = mapper;
        }
        public async Task<IEnumerable<Organization>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken = default)
        {
            return await DBContext.Organizations.ToListAsync(cancellationToken);
        }
    }
}