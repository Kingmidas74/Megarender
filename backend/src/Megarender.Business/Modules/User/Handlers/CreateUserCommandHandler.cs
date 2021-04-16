using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.Business.Providers;
using Megarender.DataAccess;
using Megarender.Domain;

namespace Megarender.Business.Modules.UserModule
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IAPIContext _dbContext;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IAPIContext dbContext, IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
        }
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            return (await _dbContext.Users.AddAsync(_mapper.Map<CreateUserCommand, User>(request), cancellationToken)).Entity;
        }
    }
}