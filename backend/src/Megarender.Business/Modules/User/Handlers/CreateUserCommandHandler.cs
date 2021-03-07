using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private IAPIContext DBContext;
        private IMapper Mapper;
        public CreateUserCommandHandler(IAPIContext dbContext, IMapper mapper)
        {
            DBContext=dbContext;
            Mapper = mapper;
        }
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = (await this.DBContext.Users.AddAsync(Mapper.Map<User>(request), cancellationToken)).Entity;
            await this.DBContext.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}