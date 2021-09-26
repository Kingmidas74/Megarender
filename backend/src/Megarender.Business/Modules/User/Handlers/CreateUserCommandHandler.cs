using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Megarender.DataAccess;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

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
            var user = _mapper.Map<CreateUserCommand, User>(request); 
            var userFromDb = await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id == user.Id, cancellationToken);
            return userFromDb !=null ? userFromDb : (await _dbContext.Users.AddAsync(user, cancellationToken)).Entity;
        }
    }
}