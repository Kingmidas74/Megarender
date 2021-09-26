using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Megarender.Business.Modules.UserModule;
using Megarender.Domain;
using Xunit;

namespace Megarender.Business.UnitTests.Modules.UserModule
{   
    public class CreateUserCommandHandlerTests:TestBaseFixture
    {
        private readonly IMapper _mapper;

        public CreateUserCommandHandlerTests()
        {
            _mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateUserCommand, User>()
            ).CreateMapper();
        }

        [Fact]
        public async Task CreateUserCommandHandler_ShouldCreateUser()
        {
            var command = Fixture.Build<CreateUserCommand>()
                .Create();

            var handler = new CreateUserCommandHandler(Context, _mapper);    
            
            var result = await handler.Handle(command);
            await Context.SaveChangesAsync();
            var createdUser = await Context.Users.FindAsync(result.Id);
            
            Context.Users.Should().HaveCount(1);
            createdUser.Should().NotBeNull();
            createdUser.Id.Should().Be(command.Id);
        }
        
        
    }
}