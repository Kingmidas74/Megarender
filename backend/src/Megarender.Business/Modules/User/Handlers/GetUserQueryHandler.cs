using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.UserModule
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
        }
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.SingleAsync(
                    new FindByIdSpecification<User>(request.Id).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}