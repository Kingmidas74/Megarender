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
        private readonly IAPIContext _apiContext;
        private readonly IMapper _mapper;
        public GetOrganizationQueryHandler(IAPIContext apiContext, IMapper mapper)
        {
            _apiContext=apiContext;
            _mapper = mapper;
        }
        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken = default)
        {
            return await _apiContext.Organizations.SingleAsync(
                    new FindByIdSpecification<Organization>(request.Id).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}