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
        private IAPIContext DBContext;
        private IMapper Mapper;
        public GetOrganizationQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            DBContext=dbContext;
            Mapper = mapper;
        }
        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken = default)
        {
            return await DBContext.Organizations.SingleAsync(
                    new FindByIdSpecification<Organization>(request.Id).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}