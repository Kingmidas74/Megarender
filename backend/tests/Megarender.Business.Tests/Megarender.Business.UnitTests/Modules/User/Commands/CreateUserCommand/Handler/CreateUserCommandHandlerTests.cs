using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.Providers;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Extensions;
using Xunit;
using Xunit.Sdk;

namespace Megarender.Business.UnitTests.Modules.User.Commands
{   
    public class CreateUserCommandHandlerTests:TestBaseFixture
    {
        private readonly Fixture _fixture = new();
        private IMapper _mapper;

        public CreateUserCommandHandlerTests()
        {
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateUserCommand, Domain.User>()
            ).CreateMapper();
        }

        [Fact]
        public async Task CreateUserCommandHandler_ShouldCreateUser()
        {
            var command = _fixture.Build<CreateUserCommand>()
                .Without(c=>c.FirstName)
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