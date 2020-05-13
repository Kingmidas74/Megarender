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
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private IAPIContext DBContext;
        private IMapper Mapper;
        public GetUserQueryHandler(IAPIContext dbContext, IMapper mapper)
        {
            DBContext=dbContext;
            Mapper = mapper;
        }
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await DBContext.Users.SingleAsync(
                    new FindByIdSpecification<User>(request.Id).IsSatisfiedByExpression,
                    cancellationToken);
        }
    }
}